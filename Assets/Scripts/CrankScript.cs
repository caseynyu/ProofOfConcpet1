using UnityEngine;

public class CrankScript : InteractableObject
{
    
    private HandControl handControl;

    [SerializeField] GameObject crankPivot;
    [SerializeField] GameObject target;
    [SerializeField] float angleOffset;

    private float lastZRotation;

    private float rotationCounter;

    void Awake()
    {
        handControl = FindAnyObjectByType<HandControl>();
    }

    public override void InteractStart()
    {
        base.InteractStart();
    }

    void Update()
    {
        if(isInteracting) UpdateRotation();
    }

    private void UpdateRotation()
    {
        //Z rotation

        Vector3 direction = target.transform.position - crankPivot.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        


        crankPivot.transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);
        //Debug.Log((lastZRotation-(crankPivot.transform.rotation.z))*10000);
        if (crankPivot.transform.rotation.z > lastZRotation)
        {
            rotationCounter+=1;
        }
        else
        {
            rotationCounter-=1;
        }

        //rotationCounter+=((Mathf.Abs(crankPivot.transform.rotation.z))-Mathf.Abs(lastZRotation));
        Debug.Log(rotationCounter);

        

        lastZRotation=crankPivot.transform.rotation.z;
        

        //crankPivot.transform.LookAt(target.transform);
    }
}
