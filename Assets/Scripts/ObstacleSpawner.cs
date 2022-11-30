using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefabs;

    [SerializeField]
    private Vector3 spawnLocation;

    [SerializeField]
    private float[] buffer = new float[] { 0f, 0f, 0f };

    private float nextSpawn;

    [SerializeField]
    private Camera main;

    private float[] path;

    private float CurrentTime;

    [SerializeField]
    private GameObject boatprefab;

    [SerializeField]
    private float boatMS = 2f;

    [SerializeField]
    private GameObject[] SpawnedObjectParents;

    [SerializeField]
    private float PowerUpSpawnChance = 10f;

    [SerializeField]
    private GameObject[] PowerUps;

    public static Dictionary<PowerUpType, int> weightTable = new Dictionary<PowerUpType, int>()
    {
        {PowerUpType.Immune, 1 },
        {PowerUpType.DoubleDamage, 20 },
        {PowerUpType.SpeedUp, 40 },
        {PowerUpType.SpeedDown, 40 },
        {PowerUpType.Heal, 40 },
    };
    void Start()
    {
        nextSpawn = Time.time;
        CurrentTime = Time.time;
        //main = Camera.current;
        float width = ((2f * main.orthographicSize) * main.aspect) / 2f;
        path = new float[] { (-width + buffer[0]), (width - buffer[1]) };
        Debug.Log(nextSpawn);
        Debug.Log(CurrentTime);

    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= nextSpawn)
        {
            if (Random.Range(0, 100) < (100 - PowerUpSpawnChance))
            {
                Vector3 pos = new Vector3(Random.Range(path[0], path[1]), main.orthographicSize - buffer[2]);
                int num = Random.Range(0, obstaclePrefabs.Length);
                Obstacle ob = obstaclePrefabs[num].GetComponent<Obstacle>();
                Instantiate(boatprefab, new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), Quaternion.identity, SpawnedObjectParents[0].transform).GetComponent<BoatMover>().SetValues(pos, boatMS, obstaclePrefabs[num], SpawnedObjectParents[1]);
                nextSpawn += Random.Range(ob.minSpawnTime, ob.maxSpawnTime) + (Vector3.Distance(new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), pos) / (boatMS * GameStateManager.msMult));
                FindObjectOfType<AudioManager>().Play("ObstacleSpawn");
            }
            else
            {
                Vector3 pos = new Vector3(Random.Range(path[0], path[1]), main.orthographicSize - buffer[2]);
                int num = PowerUpPicker();


                Instantiate(boatprefab, new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), Quaternion.identity, SpawnedObjectParents[0].transform).GetComponent<BoatMover>().SetValues(pos, boatMS, PowerUps[num], SpawnedObjectParents[2]);
                nextSpawn += PowerUps[num].GetComponent<PowerUp>().getTime() + (Vector3.Distance(new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), pos) / (boatMS * GameStateManager.msMult));
            }
        }
    }

    
    private int PowerUpPicker() {
        int[] weights = weightTable.Values.ToArrayPooled();
        int randomWeight = Random.Range(0, weights.Sum());

        for (int i = 0; i < weights.Length; i++)
        {
            randomWeight -= weights[i];
            if (randomWeight < 0)
            {
                return i;
            }
        }

        return weights.Length;
    }
}
