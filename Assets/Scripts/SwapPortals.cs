using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapPortals : MonoBehaviour
{
    private List<PortalCamera> portalCameras;
    [SerializeField] private Portal portal1,portal2;
    public bool disabled=false;
    public bool enableTriggerOnPortal1;

    void Start()
    {
        portalCameras=FindObjectsByType<PortalCamera>(FindObjectsInactive.Include).ToList();
        Reset();
    }

    public void SwapPortalsDirect()
    {
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.UpdatePortals(new Portal[] {portal1,portal2});
        }
        if (enableTriggerOnPortal1)
        {
            portal1.canTeleportThrough=true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(disabled) return;
        if(!col.gameObject.GetComponent<PlayerMovement>()) return;
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.UpdatePortals(new Portal[] {portal1,portal2});
        }
        if (enableTriggerOnPortal1)
        {
            portal1.canTeleportThrough=true;
        }
    }

    public void Reset()
    {
        if (enableTriggerOnPortal1)
        {
            portal1.canTeleportThrough=false;
        }
    }
}
