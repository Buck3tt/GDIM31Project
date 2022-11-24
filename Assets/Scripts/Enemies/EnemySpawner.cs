using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D spawnRange;
    [SerializeField]
    private List<EnemyBase> enemyPrefabs;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        SetNextSpawnTime();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawnTime)
        {
            int randomEnemyType = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[randomEnemyType], GetSpawnLocation(), Quaternion.identity);
            SetNextSpawnTime();
        }
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    private Vector3 GetSpawnLocation()
    {
        Vector2 spawnSize;
        spawnSize.x = spawnRange.transform.localScale.x * spawnRange.size.x;
        spawnSize.y = spawnRange.transform.localScale.y * spawnRange.size.y;
        Vector3 randomPos = new Vector3(Random.Range(-spawnSize.x / 2, spawnSize.x / 2), Random.Range(-spawnSize.y / 2, spawnSize.y / 2));
        return spawnRange.transform.position + randomPos;
    }
}
