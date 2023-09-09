using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject enemyGhost;
    [SerializeField] Transform enemyGameObject;
    private Transform PlayerPosition;

    private void Awake()
    {
        PlayerPosition = playerTransform;
    }

    private void Update()
    {
        if (playerTransform.position.x == transform.position.x || playerTransform.position.z == transform.position.z)
        {
            Instantiate(enemyGhost, enemyGameObject);
        }
    }

}
