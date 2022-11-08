using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, PowerUp
{
    [SerializeField]
    private float heal = 10f;

    [SerializeField]
    private float ms = 10f;

    private void Update()
    {
        transform.position -= new Vector3(ms * GameStateManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public void endpowerup()
    {
        Destroy(this.gameObject);
    }

    public void usepowerup()
    {
        FindObjectOfType<Player>().HealDamage(heal);
        endpowerup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Despawn"))
        {
            Destroy(this.gameObject);
        }
    }
}
