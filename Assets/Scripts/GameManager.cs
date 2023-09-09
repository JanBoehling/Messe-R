using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public bool IsEnemySpawned = false;

    public void GameOver()
    {
        //Gameover Scene Laden. Check in BuildSettings
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
            uiManager.AddOrgan(organIcons[organCounter - 1], organCounter);
        }
    }
    private int organCounter;

    [SerializeField] private EnemyController ghost;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Sprite[] organIcons;
    

    public void RaiseOrganCounter()
    {
        OrganCounter++;
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
    
    public void GhostSpeedUp()
    {
        ghost.speed++;
    }
}
