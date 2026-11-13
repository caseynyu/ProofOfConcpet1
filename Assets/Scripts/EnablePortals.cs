using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnablePortals : MonoBehaviour
{
    public bool disableTrigger;
    //public GameObject trigger;
    private List<PortalCamera> portalCameras;

    void Start()
    {
        portalCameras= FindObjectsByType<PortalCamera>().ToList();
    }

    void OnTriggerEnter(Collider col)
    {
        if(disableTrigger) return;
        if (gameObject.GetComponent<MovePortals>()!=null)
        {
            /* if (gameObject.GetComponent<MovePortals>().triggerEnabled)
            {
                if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
                StartCoroutine(enableDisable(col.gameObject));
            } */
        }
        else
        {
            if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
            StartCoroutine(enableDisable(col.gameObject));
        }
        //Debug.Log(col.gameObject.name);
        
    }

    public void enablePortals(GameObject player)
    {
        StartCoroutine(enableDisable(player));
    }

    private IEnumerator enableDisable(GameObject player)
    {
        yield return null;
        player.GetComponentInChildren<HandCamera>(true).gameObject.SetActive(false);
        yield return null;
        foreach(PortalCamera portalCamera in portalCameras)
        {
            portalCamera.enabled=true;
        }
        //player.GetComponentInChildren<PortalCamera>(true).gameObject.GetComponent<PortalCamera>().enabled=true;
    }
}
