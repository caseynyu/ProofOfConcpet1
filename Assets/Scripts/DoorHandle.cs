using System;
using UnityEngine;

public class DoorHandle : InteractableObject
{
    
    private float slideProgress;

    [SerializeField] Transform fullDoor;

    [SerializeField] private bool ZinsteadofX;

    public override void InteractStart()
    {
        base.InteractStart();
        //print("interacting");

    }

    void Update()
    {
        if (isInteracting && !interactionFinished)
        {
            if (!ZinsteadofX)
            {
                GetNewPositionX();
            }
            else
            {
                GetNewPositionZ();
            }
            
            
        }
    }

    private void GetNewPositionX()
    {
        Vector3 newPosition = transform.position;
        newPosition.x=handControl.handPosOnScreen(transform.position).x;
        if(newPosition.x>dragEndPoint.position.x) newPosition.x=dragEndPoint.position.x;
        if(newPosition.x<dragStartPoint.position.x) newPosition.x=dragStartPoint.position.x;
        if (newPosition.x <= dragStartPoint.position.x)
        {
            newPosition.x =dragStartPoint.position.x;
            interactionFinished=true;
            handControl.SetTriggerInteractionFinished();
        } 
        float positionDifference = transform.position.x-newPosition.x;
        fullDoor.transform.position = new Vector3(fullDoor.transform.position.x-positionDifference,fullDoor.transform.position.y,fullDoor.transform.position.z);
        transform.position = newPosition;
    }
    private void GetNewPositionZ()
    {
        Vector3 newPosition = transform.position;
        newPosition.z=handControl.handPosOnScreen(transform.position).x;
        if(newPosition.z>dragEndPoint.position.z) newPosition.z=dragEndPoint.position.z;
        if(newPosition.z<dragStartPoint.position.z) newPosition.z=dragStartPoint.position.z;
        if (newPosition.z <= dragStartPoint.position.z)
        {
            newPosition.z =dragStartPoint.position.z;
            interactionFinished=true;
            handControl.SetTriggerInteractionFinished();
        } 
        float positionDifference = transform.position.z-newPosition.z;
        fullDoor.transform.position = new Vector3(fullDoor.transform.position.z-positionDifference,fullDoor.transform.position.y,fullDoor.transform.position.z);
        transform.position = newPosition;
    }
}