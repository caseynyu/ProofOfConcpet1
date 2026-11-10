using UnityEngine;

public class MovePortalsNature : MonoBehaviour
{
    [SerializeField] Transform natureEntrance,psxExit;
    [SerializeField] GameObject portal1,portal2;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        portal1.transform.position=natureEntrance.position;
        portal1.transform.rotation=natureEntrance.rotation;
        portal2.transform.position=psxExit.position;
        portal2.transform.rotation=psxExit.rotation;
    }
}
