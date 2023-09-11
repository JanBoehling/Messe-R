using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public float CameraSensibility = 550f;

    public GameObject EnemyPrefab;
    public bool EnemyCanSpawn = false;
    public bool IsEnemySpawned = false;
    public int CounterOfOrgans;
    [Tooltip("In Seconds")]
    public float StartSpawnCooldown = 20f;

    public void ChangeCameraSensibility(float value)
    {
        CameraSensibility = value;
    }

    public void GameOver()
    {
        //Gameover Scene Laden. Check in BuildSettings
        DataHolder.GameOverMessage = "Du hast verkackt";
        SceneManager.LoadScene(4);
    }

    public void WonGame()
    {
        DataHolder.GameOverMessage = "Du hast es geschafft, der Geist der Vergangenen Weihnacht ist wieder eingesperrt.";
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Counts how many organs the player collected
    /// </summary>
    public int OrganCounter
    { 
        get => organCounter;
        set
        {
            organCounter = value;
        }
    }
    private int organCounter;

    [SerializeField] private EnemyController ghost;
    [SerializeField] private UIManager uiManager;

    public void AddOrganIcon(OrganType organType) => uiManager.AddOrgan(organType, organCounter);

    public void RaiseOrganCounter()
    {
        organCounter++;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForGhostspawning());
    }

    private IEnumerator WaitForGhostspawning()
    {
        yield return new WaitForSeconds(StartSpawnCooldown);
        EnemyCanSpawn = true;
        Debug.Log("Now the enemy can spawn!");
    }
}
