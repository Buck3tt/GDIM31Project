using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject [] obstaclePrefabs;

    [SerializeField]
    private Vector3 spawnLocation;

    private float nextSpawn;


    void Start()
    {
        nextSpawn = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawn)
        {
            int num = Random.Range(0, obstaclePrefabs.Length);
            Obstacle obstacle = Instantiate(obstaclePrefabs[num], spawnLocation, Quaternion.identity).GetComponent<Obstacle>();
            float baseNext = Random.Range(obstacle.minSpawnTime, obstacle.maxSpawnTime);
            nextSpawn += baseNext - (baseNext * (GameStateManager.msMult-1));
        }
    }
}
