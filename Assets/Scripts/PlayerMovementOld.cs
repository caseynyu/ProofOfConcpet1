using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementOld : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    private Camera mainCamera;
    private CharacterController characterController;
    public float currentMoveSpeed = 0f;
    private Vector2 currentLookVector;
    public Vector2 currentMoveVector;
    private Vector2 smoothedLook;
    private Vector2 smoothedMove;
    [SerializeField]private float smoothLookSpeed;
    [SerializeField] private float smoothMoveSpeed;
    private Vector3 moveDir = Vector3.zero;

    private InputAction moveInput;
    
    private void Start()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        mainCamera=GetComponentInChildren<Camera>();
        characterController=GetComponent<CharacterController>();
        currentMoveSpeed=moveSpeed;
    }

    private void Update()
    {
        Vector2 rawMoveVector = new Vector2(moveInput.ReadValue<Vector2>().x,moveInput.ReadValue<Vector2>().y);
        Vector2 rawLookVector = new Vector2(moveInput.ReadValue<Vector2>().x,0);

        currentLookVector = Vector2.SmoothDamp(currentLookVector, rawLookVector,ref smoothedLook,smoothLookSpeed);
        currentMoveVector = Vector2.SmoothDamp(currentMoveVector,rawMoveVector,ref smoothedMove, smoothMoveSpeed);

        if(rawLookVector.y==0) currentLookVector.y=0;
        Movement(currentMoveVector);
        Look(currentLookVector);
    }

    private void Movement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDir.y;

        Vector2 newSpeed = new Vector2(moveVector.y * moveSpeed, moveVector.x * moveSpeed);

        moveDir = (forward * newSpeed.x) + (right * newSpeed.y);
        moveDir.y = oldY;

        characterController.Move(moveDir*Time.deltaTime);
    }

    private void Look(Vector2 lookVector)
    {
        transform.rotation *= Quaternion.Euler(0,lookVector.x* lookSpeed*Time.deltaTime,0);
    }
}
