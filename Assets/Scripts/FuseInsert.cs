using UnityEngine;

public class FuseInsert : InteractableObject
{
    [SerializeField] FuseButton fuseButton;
    [SerializeField] GameObject fuseModel;
    public override void InteractOneShotTrigger()
    {
        Debug.Log("interacteddd");
        base.InteractOneShotTrigger();
        if (handControl.hasFuse)
        {
            fuseButton.fuseInserted=true;
            fuseModel.SetActive(true);
        }
    }
}
