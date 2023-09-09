using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] InterctableUI interactableUI;
    public float speed;

    public Color debugRayColor = Color.red; // Farbe für den Debug-Ray

    private SpawnArea activeSpawnArea = null;
    private NavMeshAgent navMeshAgent = null;
    private State enemyState = State.Following;
    private Vector3 lastKnownPlayerPosition;

    private GameObject player;

    private float elapsedTime = 0;

    private enum State
    {
        Following,
        SearchStanding
        //Wandering
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

        switch (enemyState)
        {
            case State.Following:
                {
                    Vector3 playerDirection = player.transform.position - transform.position;
                    RaycastHit hit;

                    Debug.DrawRay(transform.position, playerDirection, debugRayColor, 0.1f);
                    if (Physics.Raycast(transform.position, playerDirection, out hit))
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
                    
                    if (Vector3.Distance(player.transform.position, transform.position) <= 2.5f)
                    {
                        
                        if(GameManager.Instance != null)
                            GameManager.Instance.GameOver();
                    }
                    break;
                }

            case State.SearchStanding:
                {
                    Vector3 playerDirection = player.transform.position - transform.position;
                    RaycastHit hit;

                    Debug.DrawRay(transform.position, playerDirection, debugRayColor, 0.1f);
                    if (Physics.Raycast(transform.position, playerDirection, out hit))
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
