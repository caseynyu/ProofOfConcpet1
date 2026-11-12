using UnityEngine;

public class MovePortals : MonoBehaviour
{
    [SerializeField] Transform entrance,exit;
    [SerializeField] GameObject portal1,portal2;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        portal1.transform.position=entrance.position;
        portal1.transform.rotation=entrance.rotation;
        portal2.transform.position=exit.position;
        portal2.transform.rotation=exit.rotation;
    }
}
