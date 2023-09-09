using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] InterctableUI interactableUI;
    public float speed;

    private SpawnArea ActiveSpawnArea = null;

    private NavMeshAgent navMeshAgent = null;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetSpawnArea(SpawnArea spawnArea)
    {
        ActiveSpawnArea = spawnArea;
    }
}
