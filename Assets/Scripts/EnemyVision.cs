using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public int numberOfRaycasts = 8;
    public float raycastDistance = 10f;
    bool hasHitTarget;
    [SerializeField] int raycounter;
    [SerializeField] int rotation;
    [SerializeField] Transform enemyTransformer;
    public CreateSight Raycast1;
    List<CreateSight> Sights;


    void Start()
    {
        Sights = new List<CreateSight>();
        for (int i = -raycounter; i < raycounter; i++)
        {
            var Raycasts = Instantiate(Raycast1, transform.position, Quaternion.Euler(0, i * rotation, 0), enemyTransformer);
            Sights.Add(Raycasts);
        }

    }

    void Update()
    {
        foreach (CreateSight item in Sights)
        {
            hasHitTarget = item.HasHit();
        }
    }
}
    

