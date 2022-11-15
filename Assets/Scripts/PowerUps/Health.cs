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
        transform.position -= new Vector3(ms * GameSpawnnerManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public void endpowerup()
    {
        Destroy(this.gameObject);
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
