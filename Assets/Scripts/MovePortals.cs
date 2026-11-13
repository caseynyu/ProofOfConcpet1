using UnityEngine;

public class MovePortals : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] Transform entrance,exit;
    [SerializeField] GameObject portal1,portal2;
=======
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
>>>>>>> Stashed changes

    void OnTriggerEnter(Collider col)
    {
        if (!triggerEnabled) return;
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
<<<<<<< Updated upstream
        portal1.transform.position=entrance.position;
        portal1.transform.rotation=entrance.rotation;
        portal2.transform.position=exit.position;
        portal2.transform.rotation=exit.rotation;
=======
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
>>>>>>> Stashed changes
    }
}
