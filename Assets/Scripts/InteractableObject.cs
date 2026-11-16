using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracting=false;
    public HandControl.InteractionTypes interactionType;
    public HandControl.InteractionInputs InteractionInput;
    public bool interactionFinished=false;
    public HandControl handControl;

    public Transform dragStartPoint,dragEndPoint;

    public virtual void Awake()
    {
        handControl=FindAnyObjectByType<HandControl>();
    }

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