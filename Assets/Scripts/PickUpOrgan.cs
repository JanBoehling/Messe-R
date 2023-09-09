using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpOrgan : MonoBehaviour
{
    [SerializeField] private OrganType organType;
    [SerializeField] FillingTheCorpse fillingCorpses;

    public OrganType Organ => organType;
    
    public void DestroyObject()
    {
        Destroy(gameObject);
        fillingCorpses.counterOfOrgans++;
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
