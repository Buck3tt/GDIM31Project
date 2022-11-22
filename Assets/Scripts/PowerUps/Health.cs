using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, PowerUp
{
    
    [SerializeField]
    private float heal = 10f;

    [SerializeField]
    private Vector2 ms;

    [SerializeField]
    private float minTime, maxTime;

    private void Update()
    {
        transform.position -= (Vector3)(ms) * GameStateManager.msMult * Time.deltaTime;
    }

    public void endpowerup()
    {
        Destroy(gameObject);
    }

    public void usepowerup()
    {
        if (heal > 0)
            FindObjectOfType<Player>().HealDamage(heal);
        else
            FindObjectOfType<Player>().TakeDamage(heal);
        endpowerup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Despawn"))
        {
            Destroy(gameObject);
        }
    }

    public float getTime()
    {
        return Random.Range(minTime, maxTime);
    }
}
