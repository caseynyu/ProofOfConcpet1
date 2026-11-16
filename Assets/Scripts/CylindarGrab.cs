using UnityEngine;

public class CylindarGrab : InteractableObject
{
    public override void InteractOneShotTrigger()
    {
        base.InteractStart();
        print("interacted");
        interactionFinished=true;
        handControl.SetTriggerInteractionFinished();
        handControl.hasFuse=true;
        gameObject.SetActive(false);
    }
}
