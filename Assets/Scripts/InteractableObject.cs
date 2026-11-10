using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracting=false;
    public HandControl.InteractionTypes interactionType;
    public HandControl.InteractionInputs InteractionInput;

    public Transform dragStartPoint,dragEndPoint;


    public virtual void InteractStart()
    {
        isInteracting=true;
    }

    public virtual void InteractEnd()
    {
        isInteracting=false;
    }

    public virtual void InteractOneShotStart()
    {
        
    }

    public virtual void InteractOneShotTrigger()
    {
        
    }
    public virtual void InteractOneShotFail()
    {
        
    }
}