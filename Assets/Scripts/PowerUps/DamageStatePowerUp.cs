using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatePowerUp : MonoBehaviour, PowerUp
{
    [SerializeField]
    private DamageState powerUp;

    [SerializeField]
    private float durration;

    [SerializeField]
    private float ms = 10f;

    private Player player;

    private void Update()
    {
        transform.position -= new Vector3(ms * GameSpawnnerManager.msMult * Time.deltaTime, 0f, 0f);
    }
    public void endpowerup()
    {
        player.ChangeDamageState(DamageState.Normal);
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
