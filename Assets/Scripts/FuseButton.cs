using UnityEngine;

public class FuseButton : InteractableObject
{

    public bool fuseInserted=false;
    [SerializeField] GameObject portalToDisable;

    void Start()
    {
        Reset();
    }

    public override void InteractOneShotTrigger()
    {
        base.InteractOneShotTrigger();
        if (fuseInserted)
        {
            gameObject.GetComponent<EnablePortals>().EnablePortalsDirect();
            gameObject.GetComponent<SwapPortals>().SwapPortalsDirect();
            portalToDisable.SetActive(true);
            //gameObject.GetComponent<EnablePortals>().enablePortals();
        }
    }

    public void Reset()
    {
        portalToDisable.SetActive(false);
        fuseInserted=false;
    }

}
