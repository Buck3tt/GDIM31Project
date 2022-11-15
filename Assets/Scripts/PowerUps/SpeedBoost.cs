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
        transform.position -= new Vector3(ms * GameSpawnnerManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public void endpowerup()
    {
        GameSpawnnerManager.ChangeMs(-SpeedIncrease);
        Destroy(this.gameObject);
    }

    public void usepowerup()
    {
        Debug.Log("Player Hit Powerup");
        GameSpawnnerManager.ChangeMs(SpeedIncrease);
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

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void OnGameStateChanged(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Dead:
                endpowerup();
                break;
        }
    }
}
