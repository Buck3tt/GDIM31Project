using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject [] obstaclePrefabs;

    [SerializeField]
    private Vector3 spawnLocation;

    [SerializeField]
    private float [] buffer = new float[] { 0f, 0f, 0f};

    private float nextSpawn;

    [SerializeField]
    private Camera main;

    private float [] path;

    private float CurrentTime;

    [SerializeField]
    private GameObject boatprefab;

    [SerializeField]
    private float boatMS = 2f;

    [SerializeField]
    private GameObject[] SpawnedObjectParents;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    void Start()
    {
        nextSpawn = Time.time;
        CurrentTime = Time.time;
        //main = Camera.current;
        float width = ((2f*main.orthographicSize) * main.aspect)/2f;
        path = new float[] {(-width + buffer[0]), (width - buffer[1])};
        Debug.Log(nextSpawn);
        Debug.Log(CurrentTime);

    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= nextSpawn)
        {
            Vector3 pos = new Vector3(Random.Range(path[0], path[1]), main.orthographicSize - buffer[2]);
            int num = Random.Range(0, obstaclePrefabs.Length);
            Obstacle ob = obstaclePrefabs[num].GetComponent<Obstacle>();
            Instantiate(boatprefab, new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), Quaternion.identity, SpawnedObjectParents[0].transform).GetComponent<BoatMover>().SetValues(pos, boatMS, ob, SpawnedObjectParents[1]);
            nextSpawn += Random.Range(ob.minSpawnTime, ob.maxSpawnTime) + (Vector3.Distance(new Vector3(path[0] - buffer[0], main.orthographicSize - buffer[2]), pos) / (boatMS* GameSpawnnerManager.msMult));
        }
    }

    private void resetState()
    {
        nextSpawn = 0f;
        CurrentTime = Time.time;
        //main = Camera.current;
        float width = ((2f * main.orthographicSize) * main.aspect) / 2f;
        path = new float[] { (-width + buffer[0]), (width - buffer[1]) };
        Debug.Log(nextSpawn);
        Debug.Log(CurrentTime);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Menu:
                resetState();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Dead:
                resetState();
                Destroy(gameObject);
                break;
        }
    }


}
