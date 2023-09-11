using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float maxViewDistance = 15f;
    public float waitForDespawnTime = 3f;

    private float elapsedTime = 0;

    int layerMaskValue = 7; //LookThrough

    private float speedMultiplier = 0.1f;
    private float initialSpeed;

    public AudioSource audioSource;
    private float currentInterval;
    public AudioClip heartbeatSound;  // The heartbeat audio clip
    private float maxDistance;   // Maximum distance for the heartbeat to play
    float baseInterval = 4f;   // Initial interval between heartbeats

    private bool active = true;
    private bool playHeartbeat = false;
    private float elapsedTimeHeartbeat = 0f;

    Coroutine coroutine;

    private enum State
    {
        Following,
        SearchStanding
        //Wandering
    }

    private void Start()
    {
        maxDistance = maxViewDistance;
        enemyState = State.Following;
        layerMaskValue = LayerMask.NameToLayer("LookThrough");
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialSpeed = navMeshAgent.speed;
        Debug.Log("Initial Ghost speed is = " + initialSpeed);
        player =  GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player Object with Tag Player could not be found.");
        }

        currentInterval = baseInterval;
    }

    private void PlayHeartbeat()
    {
        if (playHeartbeat == false)
            return;

        if(audioSource != null && heartbeatSound != null)
        {
            Debug.Log("Play Heartbeat");
            audioSource.PlayOneShot(heartbeatSound);
        }
        else
        {
            Debug.LogError("Heartbeat or AudioSource is null");
        }
    }

    public void SetSpawnArea(SpawnArea spawnArea)
    {
        activeSpawnArea = spawnArea;
    }

    private void OnDestroy()
    {
        active = false;

        StopAllCoroutines();
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
            float newSpeed = initialSpeed + ((GameManager.Instance.OrganCounter * initialSpeed) * speedMultiplier);
            navMeshAgent.speed = newSpeed;
            
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

                    if(elapsedTime > waitForDespawnTime)
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

        // Calculate the distance between player and enemy
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Play heartbeat at intervals
        if (distance < maxDistance)
        {
            float newInterval = currentInterval;
            if (distance > 8)
            {
                newInterval = 2;
            }
            else if (distance <= 8 && distance > 4)
            {
                newInterval = 1;
            }
            else if (distance <= 4)
                newInterval = 0.5f;

            //if (currentInterval != newInterval)
            //{
            //    currentInterval = newInterval;
            //    if (coroutine != null)
            //    {
            //        StopCoroutine(coroutine);
            //        coroutine = null;
            //    }
            //    coroutine = StartCoroutine(DoHearbeat());
            //}

            if (!audioSource.isPlaying)
            {
                if (currentInterval != newInterval)
                {
                    CancelInvoke("PlayHeartbeat");
                    currentInterval = newInterval;
                }

                playHeartbeat = true;
                InvokeRepeating("PlayHeartbeat", 0f, currentInterval);
            }
        }
        else
        {
            playHeartbeat = false;
            //if(coroutine != null)
            //{
            //    StopCoroutine(coroutine);
            //    coroutine = null;
            //}
            CancelInvoke("PlayHeartbeat"); // Stop playing the heartbeat sound
        }
    }

    IEnumerator DoHearbeat()
    {
        Debug.Log("Coroutine gestartet");
        playHeartbeat = true;
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);
            Debug.Log("Current Interval = " + currentInterval);
            if (!audioSource.isPlaying)
                PlayHeartbeat();
        }
    }

}
