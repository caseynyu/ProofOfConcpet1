using UnityEngine;

public class FuseInsert : InteractableObject
{
    [SerializeField] FuseButton fuseButton;
    [SerializeField] GameObject fuseButtonRed,fuseButtonGreen;
    [SerializeField] GameObject fuseModel;

    void Start()
    {
        Reset();
    }
    public override void InteractOneShotTrigger()
    {
        //Debug.Log("interacteddd");
        base.InteractOneShotTrigger();
        if (handControl.hasFuse)
        {
            fuseButtonRed.SetActive(false);
            fuseButtonGreen.SetActive(true);
            fuseButton.fuseInserted=true;
            fuseModel.SetActive(true);
        }
    }

    public void Reset()
    {
        fuseButtonRed.SetActive(true);
        fuseButtonGreen.SetActive(false);
        fuseButton.fuseInserted=false;
        fuseModel.SetActive(false);
    }
}
