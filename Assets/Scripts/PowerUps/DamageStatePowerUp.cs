using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageStatePowerUp : MonoBehaviour, PowerUp
{

    [SerializeField]
    private Player.DamageState powerUp;

    [SerializeField]
    private float durration;

    [SerializeField]
    private Vector2 ms;

    private Player player;

    [SerializeField]
    private float minTime, maxTime;

    private float startTime;

    private void Update()
    {
        transform.position -= (Vector3)(ms) * GameStateManager.msMult * Time.deltaTime;
    }
    public void endpowerup()
    {
        player.ChangeDamageState(Player.DamageState.Normal);
        Destroy(this.gameObject);
    }

    public void usepowerup()
    {
        startTime = Time.time;
        player = FindObjectOfType<Player>();
        player.ChangeDamageState(powerUp);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(EndDamageState(player.GetHealthImage()));
    }

    IEnumerator EndDamageState (Image health)
    {
        while(Time.time - startTime < durration)
        {
            switch(health.color.a.ToString())
            {
                //0.5 is just the standard amount of time for heart to blink
                case "0":
                    health.color = new Color(health.color.r, health.color.g, health.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;

                case "1":
                    health.color = new Color(health.color.r, health.color.g, health.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
        endpowerup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Despawn"))
        {
            Destroy(this.gameObject);
        }
    }

    public float getTime()
    {
        return Random.Range(minTime, maxTime);
    }
}
