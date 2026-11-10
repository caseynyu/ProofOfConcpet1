using UnityEngine;
using System.Collections;

public class DisablePortals : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        StartCoroutine(enableDisable(col.gameObject));
        
        
    }

    private IEnumerator enableDisable(GameObject player)
    {
        player.GetComponentInChildren<PortalCamera>().gameObject.GetComponent<PortalCamera>().enabled=false;
        yield return null;
        player.GetComponentInChildren<HandCamera>(true).gameObject.SetActive(true);
    }
}
