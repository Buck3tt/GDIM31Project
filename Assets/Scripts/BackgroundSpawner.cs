using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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
            if (backgroundList.Count < 2)
                SpawnBG();
        }
        else if ((backgroundList[backgroundList.Count - 2].GetBGWidth() - backgroundList[backgroundList.Count-1].GetBGWidth()) > 0)
        {
           
            SpawnBG();
            
        }
    }

    private void SpawnBG()
    {
        backgroundList.Add(Instantiate(backgrounds[currentBG]));
        int current = backgroundList.Count - 2;
        int next = current + 1;

        float bgPos = backgroundList[current].transform.position.x + backgroundList[current].GetBGWidth() - (backgroundList[current].GetBGWidth() - backgroundList[next].GetBGWidth()) / 2f;

        backgroundList[next].transform.position = new Vector3(bgPos - (GameStateManager.msMult-1f), 0f, 0.9f);
       
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

