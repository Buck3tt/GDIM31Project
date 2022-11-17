using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    [SerializeField]
    private float[] speedCheckpoints;
    private int currentCheckpoint;

    [SerializeField]
    private string menuSceneName;

    [SerializeField]
    private string mainSceneName;

    [SerializeField]
    private int pointsPerSecond;

    [SerializeField]
    private float msChange;
    public static float msMult { get; private set; }

    enum GAMESTATE
    {
        Menu, Playing, Paused, Dead
    }
    private static GAMESTATE currentState;
    public static Action OnPause;

    private float timeStart;
    private int currentPoints;
    private float nextPoint;

    private static GameStateManager _instance;
    private BackgroundSpawner bgSpawner;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            currentState = GAMESTATE.Menu;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.TogglePause();
        }
        
        if(currentState == GAMESTATE.Playing && SpeedCheckpointReached())
        {
            msMult += msChange;
            UpdateCheckpoint();
            bgSpawner.ChangeBackground();
        }
    }

    private void UpdateCheckpoint()
    {
        if(currentCheckpoint + 1 >= speedCheckpoints.Length)
        {
            currentCheckpoint = 0;
        } else
        {
            currentCheckpoint++;
        }
    }

    private bool SpeedCheckpointReached()
    {
        //should be a more elegant way to mod the time without the 0.001f
        float modifiedTime = (Time.time - timeStart) % (speedCheckpoints[^1] + 0.001f);
        return speedCheckpoints[currentCheckpoint] <= modifiedTime;
    }

    private void FixedUpdate()
    {
        if(currentState == GAMESTATE.Playing && Time.time >= nextPoint)
        {
            currentPoints += pointsPerSecond;
            nextPoint++;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreDisplay.text = $"Score: {currentPoints}";
    }

    public static void ChangeMs(float modifier)
    {
        msMult += modifier;
    }

    public static int GetScore()
    {
        return _instance.currentPoints;
    }

    public static void SetBackgroundSpawner(BackgroundSpawner bgSpawner)
    {
        _instance.bgSpawner = bgSpawner;
    }

    public static void NewGame()
    {
        currentState = GAMESTATE.Playing;
        _instance.currentCheckpoint = 0;
        msMult = 1f;
        _instance.timeStart = Time.time;
        _instance.currentPoints = 0;
        _instance.nextPoint = _instance.timeStart + 1;
        _instance.UpdateScore();
        SceneManager.LoadScene(_instance.mainSceneName);
    }

    public static void TogglePause()
    {
        if(currentState == GAMESTATE.Playing)
        {
            currentState = GAMESTATE.Paused;
            Time.timeScale = 0;
            OnPause();
        }
        else if(currentState == GAMESTATE.Paused)
        {
            currentState = GAMESTATE.Playing;
            Time.timeScale = 1;
        }

    }

    public static void GameOver()
    {
        currentState = GAMESTATE.Menu;
        SceneManager.LoadScene(_instance.menuSceneName);
    }
}