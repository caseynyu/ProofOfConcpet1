using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HandControl : MonoBehaviour
{
    [SerializeField] private GameObject handObj;
    [SerializeField] private Camera mainCamera,handCamera;
    [SerializeField] private Transform handStartPos;

    public enum PlayStates
    {
        exploring,
        interacting,
    }
    public PlayStates currentPlayState;

    private PlayerCameraControl playerCameraControl;
    [SerializeField] private float handMoveSpeed;
    private Vector3 target;
    private Vector3 handMoveVelocity = Vector3.zero;

    void Start()
    {
        currentPlayState=PlayStates.exploring;
        playerCameraControl=GetComponent<PlayerCameraControl>();
    }

    void Update()
    {
        switch (currentPlayState){
            case PlayStates.exploring:
                if(Keyboard.current.spaceKey.wasPressedThisFrame) SwitchToInteracting(transform);
                return;
            case PlayStates.interacting:
                if(Keyboard.current.spaceKey.wasPressedThisFrame) SwitchToExploring();
                return;
        }

    }


    private void MoveHand()
    {
        target = handCamera.WorldToScreenPoint(Input.mousePosition);
        handObj.transform.position = Vector3.SmoothDamp(handObj.transform.position,target, ref handMoveVelocity, handMoveSpeed);
    }

    public void SwitchToExploring()
    {
        playerCameraControl.freezeCameraRotation=false;
        handCamera.enabled=true;
    }

    public void SwitchToInteracting(Transform interactTransform)
    {

        playerCameraControl.freezeCameraRotation=true;
        Mouse.current.WarpCursorPosition(handStartPos.position);

        handCamera.enabled=false;
    }
}
