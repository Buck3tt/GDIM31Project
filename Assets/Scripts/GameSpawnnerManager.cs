using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpawnnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] backgrounds;

    [SerializeField]
    private float[] speedCheckpoints;

    [SerializeField]
    private int PointsPerSecond;

    [SerializeField]
    private GameObject scoredisplay;

    [SerializeField]
    private float MSChange = 0.1f;

    public static float msMult { get; private set; }

    private List<Background> backgroundList;

    private int currentBG;

    private static float timestart;
    private static int currentpoints = 0;
    private static float NextPoint = 0;

    void Start()
    {
        scoredisplay.GetComponent<TextMeshProUGUI>().text = $"{currentpoints}";
        currentBG = 0;
        msMult = 1f;
        backgroundList = new List<Background>
        {
            Instantiate(backgrounds[currentBG]).GetComponent<Background>()
        };
        timestart = Time.time;
        NextPoint = timestart + 1;
        spawnBG();
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    // Update is called once per frame
    void Update()
    {
        updateBG();
    }

    private void FixedUpdate()
    {
        if (Time.time >= NextPoint) {
            currentpoints += PointsPerSecond;
            NextPoint++;
            scoredisplay.GetComponent<TextMeshProUGUI>().text = $"{currentpoints}";
        }
    }

    public static int currentScore()
    {
        return currentpoints;
    }

    private void updateBG()
    {
        if (backgroundList[0].transform.position.x <= backgroundList[0].ReSetPoint.x)
        {
            GameObject past = backgroundList[0].gameObject;
            backgroundList.RemoveAt(0);
            Destroy(past);
            spawnBG();
        }
    }

    private void spawnBG()
    {
        if(speedCheckpoints[currentBG] <= ((Time.time - timestart) % 61) && currentBG < speedCheckpoints.Length-1)
        {
            currentBG++;
            msMult += MSChange;
        }
        else if (speedCheckpoints[currentBG] <= ((Time.time - timestart) % 61))
        {
            currentBG = 0;
            msMult += MSChange;
        }
        backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
        float bgPos = backgroundList[0].transform.position.x + backgroundList[0].transform.localScale.x - (backgroundList[0].transform.localScale.x - backgroundList[1].transform.localScale.x) / 2f;
        backgroundList[1].transform.position = new Vector3(bgPos - 0.2f, 0f, 0.1f);     
    }

    public static void ChangeMs(float modifier)
    {
        Debug.Log($"Movement Speed Changed by {modifier}");
        msMult += modifier;
    }

    private void ResetGameState()
    {
        currentpoints = 0;
        timestart = Time.time;
        NextPoint = timestart + 1;
        scoredisplay.GetComponent<TextMeshProUGUI>().text = $"{currentpoints}";
        currentBG = 0;
        msMult = 1f;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        switch(newGameState)
        {
            case GameState.Menu:
                ResetGameState();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Dead:
                ResetGameState();
                Destroy(gameObject);
                break;
        }
    }

}

