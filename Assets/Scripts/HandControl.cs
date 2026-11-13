using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class HandControl : MonoBehaviour
{

    [SerializeField] private GameObject handObj,cameraHolder;
    [SerializeField] private Camera mainCamera,handCamera;
    [SerializeField] private Transform handStartPos;

    [SerializeField] private Sprite handSpriteDefault,handSpriteGrab1;

    public enum PlayStates
    {
        exploring,
        interacting,
        grabCutscene,
    }
    public PlayStates currentPlayState;

    private PlayerCameraControl playerCameraControl;
    [SerializeField] private float handMoveSpeed,playerSmoothDampSpeed,slerpSpeed;
    private Vector3 handTargetPos;
    private Transform playerTargetTrans;
    private Vector3 handMoveVelocity = Vector3.zero;
    private float playerRotateValueX,playerRotateValueY;
    private Vector3 playerMoveVelocity = Vector3.zero;

    private GameObject collidedInteractionTrigger;

    [SerializeField] private GameObject rayTestObj;

    private SpriteRenderer handSpriteRenderer;

    public bool hasFuse;

    private bool switchedThisFrame=false;

    private InteractableObject hoveringInteractableObj;

    private bool currentlyInteracting=false;

    [SerializeField] private LayerMask interactionLayerMask, crankLayerMask;

    [SerializeField] private GameObject crankPointObj;
    private CharacterController controller;

    private bool insideInteractionTrigger=false;

    public enum InteractionTypes
    {
        none,
        grabFollowX,
        grabFollowY,
        grabFollowXY,
        press,
    }

    public enum InteractionInputs
    {
        none,
        mouseClickOnUp,
        mouseDrag,
        crank,
    }

    private InteractionTypes currentInteractionType=InteractionTypes.none;
    private InteractionInputs currentInteractionInput=InteractionInputs.none;

    void Start()
    {
        playerCameraControl=GetComponent<PlayerCameraControl>();
        handSpriteRenderer=handObj.GetComponent<SpriteRenderer>();
        controller=GetComponent<CharacterController>();
        SwitchToExploring();
    }

    void Update()
    {
        switch (currentPlayState){
            case PlayStates.exploring:
                //if(Keyboard.current.spaceKey.wasPressedThisFrame) SwitchToInteracting(transform);
                break;
            case PlayStates.interacting:
                if(Keyboard.current.wKey.wasPressedThisFrame|| Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame) {
                    SwitchToExploring();
                }
                MovePlayer();
                MoveHand();
                if(!currentlyInteracting) CheckInteraction();
                InteractInput();
                DebugViewLine();
                break;
                
        }
    }


    private void InteractInput()
    {
        if(hoveringInteractableObj==null) return;
        switch (currentInteractionInput)
        {
            case InteractionInputs.mouseClickOnUp:
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    currentlyInteracting=true;
                    hoveringInteractableObj.InteractOneShotStart();
                }
                if (Mouse.current.leftButton.wasReleasedThisFrame && currentlyInteracting && CheckOverlapHandInteraction())
                {
                    currentlyInteracting=false;
                    hoveringInteractableObj.InteractOneShotTrigger();
                }
                if(Mouse.current.leftButton.wasReleasedThisFrame && currentlyInteracting && !CheckOverlapHandInteraction())
                {
                    currentlyInteracting=false;
                    hoveringInteractableObj.InteractOneShotFail();
                }
                break;
            case InteractionInputs.mouseDrag:
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    currentlyInteracting=true;
                    hoveringInteractableObj.InteractStart();
                }
                if(Mouse.current.leftButton.isPressed && currentlyInteracting)
                {
                    //if(handPosOnScreen(hoveringInteractableObj.gameObject.transform.position).x>hoveringInteractableObj.dragEndPoint.position.x) Mouse  
                }
                if(Mouse.current.leftButton.wasReleasedThisFrame && currentlyInteracting)
                {
                    currentlyInteracting=false;
                    hoveringInteractableObj.InteractEnd();
                }
                break;
            case InteractionInputs.crank:
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    currentlyInteracting=true;
                    hoveringInteractableObj.InteractStart();
                }
                if(Mouse.current.leftButton.isPressed && currentlyInteracting)
                {
                    Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, crankLayerMask))
                    {

                        Vector3 newPos = hit.point;
                        newPos.z+=0.2859991f;
                        crankPointObj.transform.position = newPos;
                    }
                    
                    //if(handPosOnScreen(hoveringInteractableObj.gameObject.transform.position).x>hoveringInteractableObj.dragEndPoint.position.x) Mouse  
                }
                if(Mouse.current.leftButton.wasReleasedThisFrame && currentlyInteracting)
                {
                    currentlyInteracting=false;
                    hoveringInteractableObj.InteractEnd();
                }
                break;
        }
    }

    private bool CheckOverlapHandInteraction()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            GameObject hoveringObj = hit.collider.gameObject;
            if (hoveringObj.GetComponent<InteractableObject>())
            {
                if (hoveringObj.GetComponent<InteractableObject>() == hoveringInteractableObj)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }

    private void DebugViewLine()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
        Debug.DrawRay(ray.origin,ray.direction * 10,Color.green);
    }


    private void CheckInteraction()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,Mathf.Infinity,interactionLayerMask))
        {
            GameObject hoveringObj = hit.collider.gameObject;
            //rayTestObj.transform.position=clickedObj.transform.position;
            if (hoveringObj.GetComponent<InteractableObject>())
            {
                hoveringInteractableObj = hoveringObj.GetComponent<InteractableObject>();
                currentInteractionType=hoveringInteractableObj.interactionType;
                currentInteractionInput=hoveringInteractableObj.InteractionInput;

                
                //collidedInteractionTrigger=hit.collider.gameObject;

                switch (currentInteractionInput)
                {
                    case InteractionInputs.none:
                        handSpriteRenderer.sprite=handSpriteDefault;
                        break;
                    case InteractionInputs.mouseClickOnUp:
                        handSpriteRenderer.sprite=handSpriteGrab1;
                        break;
                    case InteractionInputs.mouseDrag:
                        handSpriteRenderer.sprite=handSpriteGrab1;
                        break;
                    case InteractionInputs.crank:
                        handSpriteRenderer.sprite=handSpriteGrab1;
                        break;
                }
                
                
                
            }
            else
            {
                currentInteractionType=InteractionTypes.none;
                currentInteractionInput=InteractionInputs.none;
                handSpriteRenderer.sprite=handSpriteDefault;
                hoveringInteractableObj=null;
                //collidedInteractionTrigger=null;
            }
        }
        else
        {
            currentInteractionType=InteractionTypes.none;
            currentInteractionInput=InteractionInputs.none;
            handSpriteRenderer.sprite=handSpriteDefault;
            hoveringInteractableObj=null;
            //collidedInteractionTrigger=null;
        }
        


    }


    private void MovePlayer()
    {
        if(playerRotateValueY!=0) Debug.Log(playerRotateValueY);
        transform.position = Vector3.SmoothDamp(transform.position,playerTargetTrans.position, ref playerMoveVelocity, playerSmoothDampSpeed);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        //float rotY = Mathf.SmoothDampAngle(eulerAngles.y,playerTargetTrans.rotation.y,ref playerRotateValueY,playerSmoothDampSpeed);
        //float rotX = Mathf.SmoothDampAngle(cameraHolder.transform.rotation.x,playerTargetTrans.rotation.x,ref playerRotateValueX,playerSmoothDampSpeed);
        //float royYTest = 
        //Debug.Log(playerTargetTrans.rotation.y);
        Quaternion cameraHolderRotateTowards = new Quaternion(playerTargetTrans.rotation.x,cameraHolder.transform.rotation.y,cameraHolder.transform.rotation.z,cameraHolder.transform.rotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation,playerTargetTrans.rotation,slerpSpeed*Time.deltaTime);
        cameraHolder.transform.localRotation = Quaternion.identity;//Quaternion.Slerp(cameraHolder.transform.rotation,cameraHolderRotateTowards,slerpSpeed);
        //Quaternion.identity;//new Quaternion(playerTargetTrans.rotation.x,cameraHolder.transform.rotation.y,cameraHolder.transform.rotation.z,cameraHolder.transform.rotation.w);
        //cameraHolder.transform.rotation = Quaternion.Slerp(cameraHolder.transform.rotation,playerTargetTrans.rotation,slerpSpeed);
        //transform.rotation = new Quaternion(transform.rotation.x,rotY,transform.rotation.z,transform.rotation.w);
        //cameraHolder.transform.rotation = new Quaternion(rotX,transform.rotation.y,transform.rotation.z,transform.rotation.w);
    }

    private void MoveHand()
    {
        /* Vector3 screenPos = Mouse.current.position.value;
        screenPos.z = 2.5f;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        Vector3 localPos = handObj.transform.InverseTransformPoint(worldPos);

        handObj.transform.position = localPos; 
        //handTargetPos = handCamera.ScreenToViewportPoint(Mouse.current.position.value);
        //handTargetPos.z = 2.5f;
        //handObj.transform.localPosition = handTargetPos;//Vector3.SmoothDamp(handObj.transform.position,handTargetPos, ref handMoveVelocity, handMoveSpeed);
        */
        Vector3 mouseScreenPos = Mouse.current.position.value;
        mouseScreenPos.z=2.5f;
        Vector3 worldTargetPos = handCamera.ScreenToWorldPoint(mouseScreenPos);



        if (currentlyInteracting && currentInteractionInput==InteractionInputs.mouseDrag)
        {
            switch (currentInteractionType){
                case InteractionTypes.none:
                    break;
                case InteractionTypes.grabFollowX:
                    worldTargetPos.y=hoveringInteractableObj.gameObject.transform.position.y;
                    break;
                case InteractionTypes.grabFollowY:
                    worldTargetPos.x=hoveringInteractableObj.gameObject.transform.position.x;
                    break;
                case InteractionTypes.grabFollowXY:
                    break;
            }
        }
        

        handObj.transform.position=worldTargetPos;
        handObj.transform.localRotation = handCamera.transform.rotation;

    }

    public void SwitchToExploring()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity,slerpSpeed);
        cameraHolder.transform.localRotation = Quaternion.identity;
        
        controller.enabled=true;
        switchedThisFrame=true;
        currentPlayState=PlayStates.exploring;
        playerCameraControl.freezeCameraRotation=false;
        handCamera.enabled=false;
        handObj.SetActive(false);
    }

    public void SetTriggerInteractionFinished()
    {
        print(playerTargetTrans.parent.gameObject.name);
        playerTargetTrans.parent.gameObject.GetComponent<InteractionPoint>().interactionFinished=true;
    }

    public void SwitchToInteracting(Transform interactTransform)
    {
        Debug.Log("test");
        controller.enabled=false;
        switchedThisFrame=true;
        currentPlayState=PlayStates.interacting;
        playerCameraControl.freezeCameraRotation=true;
        Mouse.current.WarpCursorPosition(handStartPos.position);
        handCamera.enabled=true;
        handObj.SetActive(true);
        handObj.transform.position= new Vector3(handStartPos.position.x,handStartPos.position.y,handObj.transform.position.z);
        playerTargetTrans=interactTransform;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<InteractionPoint>()==null) return;
        if(insideInteractionTrigger) return;
        collidedInteractionTrigger = col.gameObject;
        if(collidedInteractionTrigger.GetComponent<InteractionPoint>().interactionFinished) return;
        SwitchToInteracting(collidedInteractionTrigger.GetComponent<InteractionPoint>().pointToMoveTo);
        //Debug.Log(collidedInteractionTrigger.GetComponent<InteractionPoint>().pointToMoveTo.rotation);
        insideInteractionTrigger=true;
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.GetComponent<InteractionPoint>()==null) return;
        collidedInteractionTrigger = null;
        insideInteractionTrigger=false;
    }


    public Vector3 handPosOnScreen(Vector3 hitObjPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);

        float distance = Vector3.Distance(mainCamera.transform.position,hitObjPos);
        Vector3 rayEnd = ray.GetPoint(distance);
        return rayEnd;
        //return handObj.transform.position;
    }

    public void GetInteractData()
    {
        
    }

}
