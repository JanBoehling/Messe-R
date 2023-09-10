using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Color debugRayColor = Color.red; // Farbe für den Debug-Ray

    private SpawnArea activeSpawnArea = null;
    private NavMeshAgent navMeshAgent = null;
    private State enemyState = State.Following;
    private Vector3 lastKnownPlayerPosition;

    private GameObject player;

    private float maxViewDistance = 60f;

    private float elapsedTime = 0;

    int layerMaskValue = 7; //LookThrough

    private float speedMultiplier = 0.1f;
    private float initialSpeed;

    private enum State
    {
        Following,
        SearchStanding
        //Wandering
    }

    private void Start()
    {
        enemyState = State.Following;
        layerMaskValue = LayerMask.NameToLayer("LookThrough");
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialSpeed = navMeshAgent.speed;
        player =  GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player Object with Tag Player could not be found.");
        }
    }

    public void SetSpawnArea(SpawnArea spawnArea)
    {
        activeSpawnArea = spawnArea;
    }

    private void Update()
    {
        if (navMeshAgent == null)
        {
            Debug.LogError("Nav mesh agent missing.");
            return;
        }

        if (activeSpawnArea == null)
        {
            Debug.LogError("ActiveSpawnArea missing.");
            return;
        }
        
        if(GameManager.Instance != null)
        {
            navMeshAgent.speed = initialSpeed + (GameManager.Instance.OrganCounter * speedMultiplier);
        }

        switch (enemyState)
        {
            case State.Following:
                {
                    Vector3 playerDirection = player.transform.position - transform.position;
                    RaycastHit hit;
#if UNITY_EDITOR
                    Debug.DrawRay(transform.position, playerDirection, debugRayColor, 0.1f);
#endif
                    Ray ray = new Ray(transform.position, playerDirection);
                    if (Physics.Raycast(ray, out hit, maxViewDistance, ~layerMaskValue))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            lastKnownPlayerPosition = player.transform.position;
                        }
                    }

                    if(lastKnownPlayerPosition != Vector3.zero && Vector3.Distance(lastKnownPlayerPosition, transform.position) > 1)
                    {
                        navMeshAgent.SetDestination(lastKnownPlayerPosition);
                    }
                    else
                    {
                        enemyState = State.SearchStanding;
                        Debug.Log("Changed Enemy State to SearchStanding");
                    }

                    Vector3 playerXZPos = player.transform.position;
                    playerXZPos.y = 0;

                    if (Vector3.Distance(playerXZPos, transform.position) <= 1.5f)
                    {
                        if(GameManager.Instance != null)
                            GameManager.Instance.GameOver();
                    }
                    break;
                }

            case State.SearchStanding:
                {
                    Vector3 playerXZPos = player.transform.position;
                    playerXZPos.y = 0;

                    Vector3 playerDirection = playerXZPos - transform.position;
                    RaycastHit hit;

#if UNITY_EDITOR
                    Debug.DrawRay(transform.position, playerDirection, debugRayColor, 0.1f);
#endif
                    Ray ray = new Ray(transform.position, playerDirection);
                    if (Physics.Raycast(ray, out hit, maxViewDistance, ~layerMaskValue))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            lastKnownPlayerPosition = player.transform.position;
                            enemyState = State.Following;
                            elapsedTime = 0;
                            break;
                        }
                    }

                    elapsedTime += Time.deltaTime;

                    if(elapsedTime > 5)
                    {
                        if (GameManager.Instance != null)
                        {
                            GameManager.Instance.IsEnemySpawned = false;
                        }
                        else
                            Debug.LogError("GameManager is null");
                        Destroy(this.gameObject);
                    }

                    break;
                }
        }   
    }



}
