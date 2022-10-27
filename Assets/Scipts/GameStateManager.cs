using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject [] backgrounds;

    private int currentBG;
    //private Background Background; 

    private List<Background> backgroundList;
    private int repeated;

    void Start()
    {
        //Debug.Log(spawnedBg.Count);
        backgroundList = new List<Background>();
        spawnBG();
    }

    // Update is called once per frame
    void Update()
    {
        moveBG();
    }

    private void moveBG()
    {
        //Debug.Log(backgroundList[0].transform.position);
        for (int i = 0; i< backgroundList.Count; i++)
        {
            backgroundList[i].transform.position -= new Vector3(backgroundList[0].moveSpeed * Time.deltaTime, 0f, 0f);
            //Debug.Log(backgroundList[i].transform.position);
        }
        if (backgroundList[0].transform.position.x <= backgroundList[0].ReSetPoint.x)
        {
            Debug.Log(backgroundList[0].ReSetPoint.x);
            
            GameObject past = backgroundList[0].gameObject;
            backgroundList.RemoveAt(0);
            Destroy(past);
            spawnBG();
            
        }

    }
    private void spawnBG()
    {
        if (backgroundList.Count == 0)
        {
            repeated = 0;
            currentBG = 0;
            backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());

            spawnBG();
        }
        else
        {
            repeated++;
            if (repeated >= backgroundList[0].repeats && currentBG < backgroundList.Count)
            {
                repeated = 0;
                currentBG++;
            }
            else if (repeated >= backgroundList[0].repeats)
            {
                repeated = 0;
                currentBG = 0;
            }
            /*if(repeated >= backgroundList[0].repeats)
            {
                currentBG++;
                repeated = 0;
            }*/
            backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
            backgroundList[1].transform.position = new Vector3(backgroundList[0].dimentions.x + backgroundList[1].transform.position.x, 0f, 0.1f);
        }
        
    }
}
public enum GameState
{
    Menu, Playing, Paused
}
