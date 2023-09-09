using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpOrgan : MonoBehaviour
{
    [SerializeField] private OrganType organType;
    public OrganType Organ => organType;
    
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

public enum OrganType
{
    Heart,
    Liver,
    Lung,
    Stomach,
    Kidney
}
