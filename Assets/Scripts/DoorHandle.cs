using System;
using UnityEngine;

public class DoorHandle : InteractableObject
{
    private HandControl handControl;
    private float slideProgress;

    [SerializeField] Transform fullDoor;

    void Awake()
    {
        handControl=FindAnyObjectByType<HandControl>();
    }

    public override void InteractStart()
    {
        base.InteractStart();

    }

    void Update()
    {
        if (isInteracting)
        {
            GetNewPosition();
            
        }
    }

    private void GetNewPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x=handControl.handPosOnScreen(transform.position).x;
        if(newPosition.x>dragEndPoint.position.x) newPosition.x=dragEndPoint.position.x;
        if(newPosition.x<dragStartPoint.position.x) newPosition.x=dragStartPoint.position.x;
        float positionDifference = transform.position.x-newPosition.x;
        fullDoor.transform.position = new Vector3(fullDoor.transform.position.x-positionDifference,fullDoor.transform.position.y,fullDoor.transform.position.z);
        transform.position = newPosition;
    }
}