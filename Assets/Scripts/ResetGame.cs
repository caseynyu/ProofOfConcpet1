using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] CrankScript crankScript;
    [SerializeField] List<DoorHandle> doorhandles= new List<DoorHandle>();
    [SerializeField] List<SwapPortals> swapPortals= new List<SwapPortals>();
    [SerializeField] List<InteractionPoint> interactionPoints= new List<InteractionPoint>();
    [SerializeField] FuseInsert fuseInsert;
    [SerializeField] FuseButton fuseButton;
    [SerializeField] CylindarGrab cylindarGrab;

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<PlayerMovement>()==null) return;
        foreach(DoorHandle doorHandle in doorhandles)
        {
            doorHandle.Reset();
        }
        foreach(SwapPortals swapPortals in swapPortals)
        {
            swapPortals.Reset();
        }
        foreach(InteractionPoint interactionPoint in interactionPoints)
        {
            interactionPoint.interactionFinished=false;
        }
        crankScript.Reset();
        fuseButton.Reset();
        fuseInsert.Reset();
        cylindarGrab.gameObject.SetActive(true);
    }
}
