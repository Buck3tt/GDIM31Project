using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    private Background [] backgrounds;

    private List<Background> backgroundList;

    private static int currentBG;

    void Start()
    {
        GameStateManager.SetBackgroundSpawner(this);
        currentBG = 0;
        backgroundList = new List<Background>
        {
            Instantiate(backgrounds[currentBG])
        };
        SpawnBG();
    }
    // Update is called once per frame
    void Update()
    {
        updateBG();
    }

    private void updateBG()
    {
        if (backgroundList[0].transform.position.x <= backgroundList[0].ReSetPoint.x)
        {
            GameObject pastBackground = backgroundList[0].gameObject;
            backgroundList.RemoveAt(0);
            Destroy(pastBackground);
            SpawnBG();
        }
    }

    private void SpawnBG()
    {
        backgroundList.Add(Instantiate(backgrounds[currentBG]));
        float bgPos = backgroundList[0].transform.position.x + backgroundList[0].GetBGWidth() - (backgroundList[0].GetBGWidth() - backgroundList[1].GetBGWidth()) / 2f;

        backgroundList[1].transform.position = new Vector3(bgPos+(1-GameStateManager.msMult)/2f, 0f, 0.5f);
        Debug.Log(backgroundList[0].transform.position.x + backgroundList[0].GetBGWidth());
        Debug.Log(backgroundList[0].GetBGWidth());
        Debug.Log(backgroundList[1].GetBGWidth());
    }

    public void ChangeBackground()
    {
        if(currentBG + 1 >= backgrounds.Length)
        {
            currentBG = 0;
        } else
        {
            currentBG++;
        }
    }
}

