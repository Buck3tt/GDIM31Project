using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float minTime, maxTime;

    [SerializeField]
    private Vector2 movement;

    [SerializeField]
    private float damage = 1f;


    public float minSpawnTime { get; private set; }
    public float maxSpawnTime { get; private set; }

    void Awake()
    {
        minSpawnTime = minTime;
        maxSpawnTime = maxTime;
       
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Despawn"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= (Vector3) movement * (GameSpawnnerManager.msMult) * Time.deltaTime;
    }
}
