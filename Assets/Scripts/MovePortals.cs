using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovePortals : MonoBehaviour
{
    private List<PortalCamera> portalCameras;
    [SerializeField] Portal portal1,portal2;
    public bool triggerEnabled=false;


    void Start()
    {
        portalCameras= FindObjectsByType<PortalCamera>().ToList();
        portal1.DisablePortal();
        portal2.DisablePortal();
        print("test");
    }

    void OnTriggerEnter(Collider col)
    {
        if (!triggerEnabled) return;
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.portals= new Portal[] {portal1,portal2};
        }
        portal1.EnablePortal();
        portal2.EnablePortal();
        if (gameObject.GetComponent<EnablePortals>())
        {
            gameObject.GetComponent<EnablePortals>().enablePortals(FindAnyObjectByType<HandControl>().gameObject);
        }
    }

    public void MovePortalsCode()
    {
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.portals= new Portal[] {portal1,portal2};
        }
        portal1.EnablePortal();
        portal2.EnablePortal();
    }
}
