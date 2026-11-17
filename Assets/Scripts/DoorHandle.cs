using System;
using System.Collections;
using UnityEngine;

public class DoorHandle : InteractableObject
{

    [SerializeField] Transform fullDoor;
    [SerializeField] InteractionPoint interactionPoint;

    [SerializeField] private bool ZinsteadofX;
    private Vector3 doorStartPos;
    private Vector3 handleStartPos;
    void Start()
    {
        doorStartPos=fullDoor.position;
        handleStartPos=transform.position;
    }
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

    public void Reset()
    {
        //Vector3 newPosition = transform.position;
        //newPosition.z=dragEndPoint.position.z;
        //float positionDifference = transform.position.z-newPosition.z;
        interactionPoint.interactionFinished=false;
        fullDoor.transform.position = doorStartPos;
        transform.position = handleStartPos;
        interactionFinished=false;

    }

}