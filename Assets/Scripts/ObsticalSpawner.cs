using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject [] obsticalsPrefab;

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
            int num = Random.Range(0, obsticalsPrefab.Length);
            Obstical obsticle = Instantiate(obsticalsPrefab[num], spawnLocation, Quaternion.identity).GetComponent<Obstical>();
            float baseNext = Random.Range(obsticle.minSpawnTime, obsticle.maxSpawnTime);
            nextSpawn += baseNext - (baseNext * (BackgroundSpawner.msMult-1));
        }
    }
}
