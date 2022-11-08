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
    private float ms = 10f;

    private void Update()
    {
        transform.position -= new Vector3(ms * GameStateManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public void endpowerup()
    {
        GameStateManager.ChangeMs(-SpeedIncrease);
        Destroy(this.gameObject);
    }

    public void usepowerup()
    {
        Debug.Log("Player Hit Powerup");
        GameStateManager.ChangeMs(SpeedIncrease);
        StartCoroutine(endboost());
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
            Destroy(this.gameObject);
        }
    }
}
