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
    [Range(5.0f, 100.0f)]
    [Tooltip("Must be smaller then SpawnRadius")]
    public float InnerRadiusOffset = 5;
    [Range(5.0f, 200.0f)]
    public float FollowRadius = 50;
    [Range(10.0f, 50.0f)]
    public float SpawnDistanceToPlayer = 15;

    private GameObject enemyPrefab;

    private GameObject player;

    private const string ENEMY_ASSET_NAME = "Enemy";

    public GameObject GetPlayer()
    {
        return player; 
    }

    private void Awake()
    {
        string[] guids = AssetDatabase.FindAssets(ENEMY_ASSET_NAME);

        if(guids.Length > 0 )
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            enemyPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        }
        else
        {
            Debug.LogError($"Could not find Enemy Asset with name {ENEMY_ASSET_NAME}");
        }
    }

    private void Start()
    {
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

        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.IsEnemySpawned)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= SpawnRadius)
        {
            float angle = Mathf.Acos((SpawnRadius * SpawnRadius + distanceToPlayer * distanceToPlayer - SpawnDistanceToPlayer * SpawnDistanceToPlayer) / (2 * SpawnRadius * SpawnDistanceToPlayer));

            float randomAngle = Random.Range(-angle, angle);
            float randomDistance = Random.Range(InnerRadiusOffset, SpawnRadius);

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomDistance * Mathf.Cos(randomAngle),
                transform.position.y,
                transform.position.z + randomDistance * Mathf.Sin(randomAngle)
            );

            // Ensure the spawn position is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, SpawnRadius, NavMesh.AllAreas))
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
