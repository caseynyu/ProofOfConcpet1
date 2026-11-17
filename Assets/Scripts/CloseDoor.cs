using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    [SerializeField] DoorHandle doorHandle;
    [SerializeField] AudioClip doorClose;
    

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMovement>()==null) return;
        //doorHandle.GetComponent<AudioSource>().PlayOneShot(doorClose);
        doorHandle.Reset();
    }
}
