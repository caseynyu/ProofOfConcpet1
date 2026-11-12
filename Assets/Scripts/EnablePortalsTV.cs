using System.Collections;
using UnityEngine;

public class EnablePortalsTV : MonoBehaviour
{
    public bool disableTrigger;
    public GameObject trigger;

    [SerializeField] SpriteRenderer tvStaticSprite;
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
        //Fade out tv static sprite
    }
}
