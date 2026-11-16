using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DisablePortals : MonoBehaviour
{
    private List<PortalCamera> portalCameras;
    private HandCamera handCamera;
    public bool disabled=false;
    void Start()
    {
        portalCameras= FindObjectsByType<PortalCamera>(FindObjectsInactive.Include).ToList();
        handCamera=FindAnyObjectByType<HandCamera>(FindObjectsInactive.Include);
    }   
    void OnTriggerEnter(Collider col)
    {
        if(disabled) return;
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        StartCoroutine(enableDisable());
    }

    public void DisablePortalsDirect()
    {
        StartCoroutine(enableDisable());
    }

    private IEnumerator enableDisable()
    {
        foreach(PortalCamera portalCamera in portalCameras)
        {
            LayerMask oldMask = portalCamera.gameObject.GetComponent<Camera>().cullingMask;
            portalCamera.enabled=false;
            portalCamera.gameObject.GetComponent<Camera>().cullingMask = oldMask & ~(1<<10);

        }
        yield return null;
        //handCamera.gameObject.SetActive(true);
    }
}
