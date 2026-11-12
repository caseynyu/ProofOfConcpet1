using Unity.VisualScripting;
using UnityEngine;

public class CrankScript : InteractableObject
{
    
    private HandControl handControl;

    [SerializeField] GameObject crankPivot;
    [SerializeField] GameObject target;
    [SerializeField] float angleOffset;
    [SerializeField] GameObject sunObj;
    [SerializeField] Transform sunStart,sunEnd;

    private bool stopSunChanging=false;

    [SerializeField] GameObject triggertoEnable,triggertoDisable;



    private float lastZRotation;

    private float rotationCounter;

    void Awake()
    {
        handControl = FindAnyObjectByType<HandControl>();
        sunObj.transform.position=sunStart.transform.position;
        sunObj.transform.localScale=sunStart.transform.localScale;
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
        if(stopSunChanging) return;
        //Z rotation

        Vector3 direction = target.transform.position - crankPivot.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float deltaAngle = Mathf.DeltaAngle(lastZRotation, angle + angleOffset);


        crankPivot.transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);
        //Debug.Log((lastZRotation-(crankPivot.transform.rotation.z))*10000);
        /* if (crankPivot.transform.rotation.z > lastZRotation)
        {
            rotationCounter+=1;
        }
        else if (crankPivot.transform.rotation.z < lastZRotation)
        {
            rotationCounter-=1;
        } */

        if (deltaAngle > 0)
        {
            rotationCounter += 1;
        } else if (deltaAngle < 0)
        {

            rotationCounter -= 1;
        }

        lastZRotation = crankPivot.transform.rotation.eulerAngles.z;

        MoveSun();

        //rotationCounter+=((Mathf.Abs(crankPivot.transform.rotation.z))-Mathf.Abs(lastZRotation));
        //Debug.Log(rotationCounter);
        //Debug.Log(crankPivot.transform.rotation.ToEuler());
        //Debug.Log(angle);

        //lastZRotation=crankPivot.transform.rotation.z;

        //MoveSun();
        

        //crankPivot.transform.LookAt(target.transform);
    }

    private void MoveSun()
    {
        if(rotationCounter<0) rotationCounter=0;
        if(rotationCounter>350) rotationCounter=360; 
        /* if(rotationCounter>20) sunSprite = sunspriteanimation2
        if(rotationCounter>70) sunSprite = sunspriteanimation3
        if(rotationCounter>120) sunSprite = sunspriteanimation4 */
        float rotaionCounterSmall = rotationCounter/360;

        //Use rotation counter to animate sun

        /* if(rotationCounter>140) {
            stopSunChanging=true;
            rotationCounter=150;
            triggertoEnable.SetActive(true);
            triggertoDisable.SetActive(false);
        } */

        sunObj.transform.position = Vector3.Slerp(sunStart.position,sunEnd.position,rotaionCounterSmall);
        sunObj.transform.localScale = Vector3.Slerp(sunStart.localScale,sunEnd.localScale,rotaionCounterSmall);
        

    }
}
