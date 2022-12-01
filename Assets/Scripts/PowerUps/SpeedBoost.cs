using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, PowerUp
{
    
    
    [SerializeField]
    private float durration = 2f;

    [SerializeField]
    private float SpeedIncrease = 0.2f;

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
        GameStateManager.ChangeMs(-SpeedIncrease);
        Destroy(gameObject);
    }

    public void usepowerup()
    {
        Debug.Log("Player Hit Powerup");
        GameStateManager.ChangeMs(SpeedIncrease);
        StartCoroutine(endboost());
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<AudioManager>().Play("SpeedUp");
    }

    IEnumerator endboost()
    {
        yield return new WaitForSeconds(durration);
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
