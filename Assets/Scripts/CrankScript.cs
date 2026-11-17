using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrankScript : InteractableObject
{
    
    //private HandControl handControl;
    [SerializeField] List<GameObject> objectsToEnable = new List<GameObject>();
    [SerializeField] SpriteRenderer sunSpriteRend;
    [SerializeField] List<Sprite> sunSprites = new List<Sprite>();

    [SerializeField] GameObject crankPivot;
    [SerializeField] GameObject target;
    [SerializeField] float angleOffset;
    [SerializeField] GameObject sunObj;
    [SerializeField] Transform sunStart,sunEnd;

    private bool stopSunChanging=false;

    [SerializeField] GameObject triggertoEnable,secondtriggertoEnable,triggertoDisable;



    private float lastZRotation;

    private float rotationCounter;

    private Quaternion startRotation;
    [SerializeField] InteractionPoint interactionPoint;

    public override void Awake()
    {
        base.Awake();
        
        startRotation=crankPivot.transform.rotation;
        handControl = FindAnyObjectByType<HandControl>();
        sunObj.transform.position=sunStart.transform.position;
        sunObj.transform.localScale=sunStart.transform.localScale;
        //triggertoEnable.GetComponent<EnablePortals>().disabled=true;
    }

    public void Start()
    {
        Reset();
    }

    public override void InteractStart()
    {
        base.InteractStart();
    }

    void Update()
    {
        GetComponent<AudioSource>().Pause();
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
        GetComponent<AudioSource>().Pause();
        if (deltaAngle > 0)
        {
            rotationCounter += 1;
            GetComponent<AudioSource>().Play();
        } else if (deltaAngle < 0)
        {
            GetComponent<AudioSource>().Play();
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

    public void Reset()
    {
        foreach(GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
        crankPivot.transform.rotation=startRotation;
        rotationCounter=0;
        stopSunChanging=false;
        sunSpriteRend.sprite=sunSprites[0];
        interactionFinished=false;
        sunObj.transform.position = sunStart.position;
        sunObj.transform.localScale = sunStart.lossyScale;

        interactionPoint.interactionFinished=false;
    }

    private void MoveSun()
    {
        if(rotationCounter<0) rotationCounter=0;
        if (rotationCounter > 36)
        {
            sunSpriteRend.sprite=sunSprites[0];
        }
        if (rotationCounter > 72)
        {
            sunSpriteRend.sprite=sunSprites[1];
        }
        if (rotationCounter > 108)
        {
            sunSpriteRend.sprite=sunSprites[2];
        }
        if (rotationCounter > 144)
        {
            sunSpriteRend.sprite=sunSprites[3];
        }
        if (rotationCounter > 180)
        {
            sunSpriteRend.sprite=sunSprites[4];
        }
        if (rotationCounter > 216)
        {
            sunSpriteRend.sprite=sunSprites[5];
        }
        if (rotationCounter > 252)
        {
            sunSpriteRend.sprite=sunSprites[6];
        }
        if (rotationCounter > 288)
        {
            sunSpriteRend.sprite=sunSprites[7];
        }
        if (rotationCounter > 324)
        {
            sunSpriteRend.sprite=sunSprites[8];
        }

        if (rotationCounter > 350)
        {
            sunSpriteRend.sprite=sunSprites[9];
            foreach(GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
            rotationCounter=360; 
            stopSunChanging=true;
            interactionFinished=true;
            sunObj.transform.position = sunEnd.position;
            sunObj.transform.localScale = sunEnd.lossyScale;
            handControl.SetTriggerInteractionFinished();
            triggertoEnable.GetComponent<EnablePortals>().EnablePortalsDirect();
            secondtriggertoEnable.GetComponent<SwapPortals>().SwapPortalsDirect();
            //triggertoEnable.GetComponent<EnablePortals>().enablePortals(handControl.gameObject);
        }
        else
        {
            float rotaionCounterSmall = rotationCounter/360;
            sunObj.transform.position = Vector3.Slerp(sunStart.position,sunEnd.position,rotaionCounterSmall);
            sunObj.transform.localScale = Vector3.Slerp(sunStart.localScale,sunEnd.localScale,rotaionCounterSmall);
        }
        /* if(rotationCounter>20) sunSprite = sunspriteanimation2
        if(rotationCounter>70) sunSprite = sunspriteanimation3
        if(rotationCounter>120) sunSprite = sunspriteanimation4 */
        

        //Use rotation counter to animate sun

        /* if(rotationCounter>140) {
            stopSunChanging=true;
            rotationCounter=150;
            triggertoEnable.SetActive(true);
            triggertoDisable.SetActive(false);
        } */

        
        

    }
}
