using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HandControl : MonoBehaviour
{
    [SerializeField] private GameObject handObj,cameraHolder;
    [SerializeField] private Camera mainCamera,handCamera;
    [SerializeField] private Transform handStartPos;

    public enum PlayStates
    {
        exploring,
        interacting,
    }
    public PlayStates currentPlayState;

    private PlayerCameraControl playerCameraControl;
    [SerializeField] private float handMoveSpeed,playerSmoothDampSpeed;
    private Vector3 handTargetPos;
    private Transform playerTargetTrans;
    private Vector3 handMoveVelocity = Vector3.zero;
    private float playerRotateValueX,playerRotateValueY;
    private Vector3 playerMoveVelocity = Vector3.zero;

    void Start()
    {
        playerCameraControl=GetComponent<PlayerCameraControl>();
        SwitchToExploring();
    }

    void Update()
    {
        switch (currentPlayState){
            case PlayStates.exploring:
                if(Keyboard.current.spaceKey.wasPressedThisFrame) SwitchToInteracting(transform);
                return;
            case PlayStates.interacting:
                if(Keyboard.current.spaceKey.wasPressedThisFrame) {
                    SwitchToExploring();
                }
                MovePlayer();
                    MoveHand();
                return;
        }

    }

    private void MovePlayer()
    {
        transform.position = Vector3.SmoothDamp(transform.position,playerTargetTrans.position, ref playerMoveVelocity, playerSmoothDampSpeed);
        float rotY = Mathf.SmoothDamp(transform.rotation.y,playerTargetTrans.rotation.y,ref playerRotateValueY,playerSmoothDampSpeed);
        float rotX = Mathf.SmoothDamp(cameraHolder.transform.rotation.x,playerTargetTrans.rotation.x,ref playerRotateValueX,playerSmoothDampSpeed);
        transform.rotation = new Quaternion(transform.rotation.x,rotY,transform.rotation.z,transform.rotation.w);
        cameraHolder.transform.rotation = new Quaternion(rotX,transform.rotation.y,transform.rotation.z,transform.rotation.w);
    }

    private void MoveHand()
    {
        
        handTargetPos = handCamera.WorldToScreenPoint(Input.mousePosition);
        handObj.transform.position = Vector3.SmoothDamp(handObj.transform.position,handTargetPos, ref handMoveVelocity, handMoveSpeed);
    }

    public void SwitchToExploring()
    {
        currentPlayState=PlayStates.exploring;
        playerCameraControl.freezeCameraRotation=false;
        handCamera.enabled=false;
        handObj.SetActive(false);
    }

    public void SwitchToInteracting(Transform interactTransform)
    {
        currentPlayState=PlayStates.interacting;
        playerCameraControl.freezeCameraRotation=true;
        Mouse.current.WarpCursorPosition(handStartPos.position);
        handCamera.enabled=true;
        handObj.SetActive(true);
        playerTargetTrans=interactTransform;
    }
}
