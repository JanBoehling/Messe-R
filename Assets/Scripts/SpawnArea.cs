using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SpawnArea : MonoBehaviour
{
    [Range(5.0f, 100.0f)]
    public float SpawnRadius = 30;
    [Range(0.0f, 100.0f)]
    [Tooltip("Must be smaller then SpawnRadius")]
    public float InnerRadiusOffset = 5;
    [Range(10.0f, 50.0f)]
    public float SpawnDistanceToPlayer = 15;
    [Range(10.0f, 100.0f)]
    public float StartSpawnRadius = 50;

    private GameObject enemyPrefab;
    private GameObject player;

    public GameObject GetPlayer()
    {
        return player; 
    }

    private void Start()
    {
        enemyPrefab = GameManager.Instance.EnemyPrefab;

        if (enemyPrefab == null)
        {
            Debug.LogError($"Could not get enemyPrefab from GameManager");
        }

        // Try to find the player GameObject with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Check if the player GameObject was found
        if (player != null)
        {
            // Player exists, you can do something with it here
            Debug.Log("Player GameObject found.");
        }
        else
        {
            // Player doesn't exist, show an error message
            Debug.LogError("Player GameObject not found. Make sure it is tagged as 'Player' in the scene.");
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        if (enemyPrefab == null)
            return;

        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.IsEnemySpawned)
            return;

        if (GameManager.Instance.EnemyCanSpawn == false)
            return;
       
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= StartSpawnRadius)
        {
            //float angle = Mathf.Acos((SpawnRadius * SpawnRadius + distanceToPlayer * distanceToPlayer - SpawnDistanceToPlayer * SpawnDistanceToPlayer) / (2 * SpawnRadius * SpawnDistanceToPlayer));

            float randomAngle = Random.Range(0, 360);
            float randomDistance = Random.Range(InnerRadiusOffset, SpawnRadius);

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomDistance * Mathf.Cos(randomAngle),
                transform.position.y,
                transform.position.z + randomDistance * Mathf.Sin(randomAngle)
            );

            //Debug.Log("Spawn Position: " + spawnPosition);

            // Ensure the spawn position is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 30, NavMesh.AllAreas))
            {
                // Spawn the enemy at the valid position
                GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);

                GameManager.Instance.IsEnemySpawned = true;

                // Pass the location of the SpawnArea and its follow Radius to the enemy
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.SetSpawnArea(this);
                }
            }
        }
    }
}
