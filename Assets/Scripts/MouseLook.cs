using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    void Start()
    {
        //Locks and hides cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Syncing initial angles with the object's starting orientation
        Vector3 angles = transform.localEulerAngles;
        xRotation = angles.x;
        yRotation = angles.y;
    }
    
    void Update()
    {
        if (Mouse.current == null) return; //Safety: in case no mouse connected
        
        // Re-locks the mouse if clicking inside the game
        if (Mouse.current.leftButton.wasPressedThisFrame && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        //Gets mouse position
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;
        
        //Pitch (Up / Down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //prevents flipping the camera
        
        // Yaw (Left / Right)
        yRotation += mouseX;
        
        //Yaw
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
