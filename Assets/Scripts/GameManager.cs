using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public bool EnemyCanSpawn = false;
    public bool IsEnemySpawned = false;
    public int CounterOfOrgans;
    [Tooltip("In Seconds")]
    public float StartSpawnCooldown = 20f;

    public void GameOver()
    {
        //Gameover Scene Laden. Check in BuildSettings
        SceneManager.LoadScene(4);
    }

    public void WonGame()
    {
        DataHolder.GameOverMessage = "Du hast es geschafft, der Geist der Vergangenen Weinacht ist wieder eingesperrt.";
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
    [SerializeField] private Sprite[] organIcons;

    public void AddOrganIcon(OrganType organType) => uiManager.AddOrgan(organIcons[(int)organType], organCounter);

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

    IEnumerator WaitForGhostspawning()
    {
        yield return new WaitForSeconds(StartSpawnCooldown);
        EnemyCanSpawn = true;
        Debug.Log("Now the enemy can spawn!");
    }
}
