using System.Collections;
using UnityEngine;

public class EnablePortals : MonoBehaviour
{
    public bool disableTrigger;
    public GameObject trigger;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        StartCoroutine(enableDisable(col.gameObject));
        if (disableTrigger)
        {
            trigger.SetActive(false);
        }
        
    }

    private IEnumerator enableDisable(GameObject player)
    {
        player.GetComponentInChildren<HandCamera>().gameObject.SetActive(false);
        yield return null;
        player.GetComponentInChildren<PortalCamera>().gameObject.GetComponent<PortalCamera>().enabled=true;
    }
}
