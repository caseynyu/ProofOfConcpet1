using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    
    [SerializeField] public List<GameObject> interactableObjects = new List<GameObject>();
    [HideInInspector] public Transform pointToMoveTo;

    void Start()
    {
        pointToMoveTo = transform.GetChild(0);
    }
}