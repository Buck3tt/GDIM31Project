using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player = FindObjectOfType<Player>();
        player.ChangeDamageState(powerUp);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(EndDamageState());
    }

    IEnumerator EndDamageState ()
    {
        yield return new WaitForSeconds(durration);
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
