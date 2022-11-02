using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] backgrounds;

    [SerializeField]
    private float[] speedCheckpoints;

    [SerializeField]
    private int PointsPerSecond;

    [SerializeField]
    private GameObject scoredisplay;

    public static float msMult { get; private set; }

    private List<Background> backgroundList;

    private int currentBG;

    private float timestart;
    private static int currentpoints = 0;

    void Start()
    {
        currentBG = 0;
        msMult = 1f;
        backgroundList = new List<Background>();
        backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
        timestart = Time.time;
        spawnBG();
    }

    // Update is called once per frame
    void Update()
    {
        updateBG();
    }

    private void FixedUpdate()
    {
        if (Time.time == (int)Time.time)
        {
            currentpoints += PointsPerSecond;
        }
        scoredisplay.GetComponent<TextMeshProUGUI>().text = $"{currentpoints}";
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
        if(speedCheckpoints[currentBG] <= (Time.realtimeSinceStartup % 61) && currentBG < speedCheckpoints.Length-1)
        {
            currentBG++;
            msMult += .5f;
        }
        else if (speedCheckpoints[currentBG] <= (Time.realtimeSinceStartup % 61))
        {
            currentBG = 0;
            msMult += .5f;
        }
        backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
        float bgPos = backgroundList[0].transform.position.x + backgroundList[0].transform.localScale.x - (backgroundList[0].transform.localScale.x - backgroundList[1].transform.localScale.x) / 2f;
        backgroundList[1].transform.position = new Vector3(bgPos - 0.2f, 0f, 0.1f);     
    }
}
public enum GameState
{
    Menu, Playing, Paused
}
