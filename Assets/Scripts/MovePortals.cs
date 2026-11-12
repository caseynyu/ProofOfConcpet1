using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovePortals : MonoBehaviour
{
    private List<PortalCamera> portalCameras;
    [SerializeField] Portal portal1,portal2;


    void Start()
    {
        portalCameras= FindObjectsByType<PortalCamera>().ToList();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.portals= new Portal[] {portal1,portal2};
        }
    }
}
