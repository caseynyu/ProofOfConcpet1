using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class ChangeCameraRenderer : MonoBehaviour
{
    [SerializeField] int rendererToChangeTo;
    private List<PortalCamera> portalCameras;
    public bool disabled=false;

    void Start()
    {
        portalCameras= FindObjectsByType<PortalCamera>(FindObjectsInactive.Include).ToList();
    }

    void OnTriggerEnter(Collider col)
    {
        if(!col.gameObject.GetComponent<PlayerMovement>()) return;
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.gameObject.GetComponent<Camera>().GetUniversalAdditionalCameraData().SetRenderer(rendererToChangeTo);
        }

    }
}
