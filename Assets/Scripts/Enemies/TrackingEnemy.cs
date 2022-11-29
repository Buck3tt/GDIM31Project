using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy : EnemyBase
{
    [SerializeField]
    private float duration;

    private Player player;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= duration)
        {
            Destroy(gameObject);
        }
        transform.position += GameStateManager.msMult * moveSpeed * Time.deltaTime * (player.transform.position - transform.position);
    }
}
