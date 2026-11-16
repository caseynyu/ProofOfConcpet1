using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.Universal;


public class ChangePortalCameraRenderer : MonoBehaviour
{
    [SerializeField] int rendererToChangeTo;
    [SerializeField] Camera portalCamera;
    public bool disabled=false;

    void OnTriggerEnter(Collider col)
    {
        if(!col.gameObject.GetComponent<PlayerMovement>()) return;

            portalCamera.gameObject.GetComponent<Camera>().GetUniversalAdditionalCameraData().SetRenderer(rendererToChangeTo);
    }
}
