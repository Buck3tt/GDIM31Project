using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject [] backgrounds;

    [SerializeField]
    private float baseSpeed = 10f;

    //public GameStateManager instance;
    //private static GameObject[] backgrounds;
    //private static float baseSpeed;

    private int currentBG;
    private float msMult = 1f;
    //private Background Background; 

    private List<Background> backgroundList;
    private int repeated;


    void Start()
    {
        //Debug.Log(spawnedBg.Count);
        //instance = this;
        backgroundList = new List<Background>();
        spawnBG();
    }

    /*public void OnAfterDeserialize()
    {
        backgrounds = bgPrefabs;
        baseSpeed = defaultSpeed;
    }*/

    // Update is called once per frame
    void Update()
    {
        //moveBG();
    }

    private void moveBG()
    {
        //Debug.Log(backgroundList[0].transform.position);
        for (int i = 0; i< backgroundList.Count; i++)
        {
            backgroundList[i].transform.position -= new Vector3(baseSpeed * msMult * Time.deltaTime, 0f, 0f);
            //Debug.Log(backgroundList[i].transform.position);
        }
        if (backgroundList[0].transform.position.x <= backgroundList[0].ReSetPoint.x)
        {
            //Debug.Log(backgroundList[0].ReSetPoint.x);
            

            
            

        }

    }

    public void nextBG ()
    {
        GameObject past = backgroundList[0].gameObject;
        backgroundList.RemoveAt(0);
        Destroy(past);
        spawnBG();
    }
    private void spawnBG()
    {
        if (backgroundList.Count == 0)
        {
            repeated = 0;
            currentBG = 0;
            backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
            msMult += backgroundList[0].speedMult;
            spawnBG();
        }
        else
        {
            repeated++;
            
            if (repeated >= backgroundList[0].repeats && currentBG < backgrounds.Length-1)
            {
                repeated = 0;
                currentBG++;
                
            }
            else if (repeated >= backgroundList[0].repeats)
            {
                repeated = 0;
                currentBG = 0;

            }
            
            backgroundList.Add(Instantiate(backgrounds[currentBG]).GetComponent<Background>());
            //backgroundList[1].transform.position = new Vector3(backgroundList[0].dimentions.x + backgroundList[1].transform.position.x - (msMult -0.1f), 0f, 0.1f);
            float bgPos = backgroundList[0].transform.position.x + backgroundList[0].transform.localScale.x - (backgroundList[0].transform.localScale.x - backgroundList[1].transform.localScale.x) / 2f;
            backgroundList[1].transform.position = new Vector3(bgPos, 0f, 0.1f);
            if (repeated == 1 || backgroundList[0].repeats == 1)
            {
                msMult += backgroundList[0].speedMult;
            }

        }
        SetAllMoveable();
        
    }

    private void SetAllMoveable ()
    {
        var moveAble = FindObjectsOfType<MonoBehaviour>().OfType<MovementObject>();

        foreach(MovementObject move in moveAble)
        {
            move.SetMovement(baseSpeed * msMult);
        }
    }
}
public enum GameState
{
    Menu, Playing, Paused
}
