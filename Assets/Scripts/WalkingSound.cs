using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private Vector3 lastPosition;
    private AudioSource audioSource;
    private float minSpeed = 0.1f;
    
    void Start()
    {
        lastPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;
        float speed = delta.magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (speed > minSpeed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
