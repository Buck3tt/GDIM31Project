using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float movementForce;

    private Vector3 toApplyMove;

    [SerializeField]
    private float maxHealth = 10f;

    private float currentHealth;

    [SerializeField]
    private Image hp;

    [SerializeField]
    private Camera mainCamera;

    //private List<Bonus>
    public enum DamageState
    {
        Normal, DoubledDamage, Immune
    }
    [SerializeField]
    private DamageState state = DamageState.Normal;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void resetValues ()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.position += new Vector2(Input.GetAxis("Horizontal") * movementForce, Input.GetAxis("Vertical") * movementForce);

        //binds movement within camera frame, numbers could use some adjusting to be more exact
        float minY = -mainCamera.orthographicSize + gameObject.transform.localScale.y;
        float maxY = minY * -1;
        float minX = -(mainCamera.orthographicSize * mainCamera.aspect) + gameObject.transform.localScale.x;
        float maxX = minX * -1;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, minX, maxX), Mathf.Clamp(rb.position.y, minY, maxY));
    }


    public void TakeDamage(float damage)
    {
        /*if (!damageable)
        {
            return;
        }*/
        switch (state) {
            case DamageState.Normal:
                currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
                break;
            case DamageState.DoubledDamage:
                currentHealth = Mathf.Clamp(currentHealth - (2 * damage), 0, maxHealth);
                break;
            case DamageState.Immune:
                return;
        }
        

        if (currentHealth == 0f)
        {
            Death();
        }
        else
        {
            UpdateHealth();
        }
    }

    public void HealDamage(float heal)
    {
        
        currentHealth = Mathf.Clamp(currentHealth+heal, 0, maxHealth);
        UpdateHealth();
        Debug.Log($"Play healed for max of {heal} health");
    }

    private void UpdateHealth()
    {
        hp.fillAmount = currentHealth / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Powerup"))
        {
            collision.gameObject.GetComponent<PowerUp>().usepowerup();
        }
    }
    public void Death()
    {
        //GameStateManager.currentScore()
        PlayerData data = new PlayerData(GameStateManager.GetScore());
        SaveSystem.SavePlayerHighScore(data);
        GameStateManager.GameOver();
        //Destroy(this.gameObject);
        //GameStateManager.Instance.SetState(GameState.Dead);
    }

    public void ChangeDamageState(DamageState state)
    {
        Debug.Log($"The player damage state has changed to {state}");
        this.state = state;
        switch (state)
        {
            case DamageState.Normal:
                hp.color = new Color(255, 255, 255, 1);
                break;
            case DamageState.DoubledDamage:
                break;
            case DamageState.Immune:
                hp.color = new Color(255, 255, 0, 1);
                break;
        }
    }

    public DamageState GetPlayerState()
    {
        return state;
    }

    public Image GetHealthImage()
    {
        return hp;
    }
}

