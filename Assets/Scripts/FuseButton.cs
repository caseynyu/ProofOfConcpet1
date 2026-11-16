using UnityEngine;

public class FuseButton : InteractableObject
{

    public bool fuseInserted=false;


    
    public override void InteractOneShotTrigger()
    {
        base.InteractOneShotTrigger();
        if (fuseInserted)
        {
            gameObject.GetComponent<MovePortals>().MovePortalsCode();
            //gameObject.GetComponent<EnablePortals>().enablePortals();
        }
    }
}
