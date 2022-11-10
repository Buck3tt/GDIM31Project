using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float movementForce;

    private Vector3 toApplyMove;

    [SerializeField]
    private float maxHealth = 10f;

    private float currentHealth;

    [SerializeField]
    private Image hp;

    private DamageState state = DamageState.Normal;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }


    private void FixedUpdate()
    {
        rb.position += new Vector2(Input.GetAxis("Horizontal") * movementForce, Input.GetAxis("Vertical") * movementForce);
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
            hp.fillAmount = currentHealth / maxHealth;
        }
    }

    public void HealDamage(float heal)
    {
        
        currentHealth = Mathf.Clamp(currentHealth+heal, 0, maxHealth);
        Debug.Log($"Play healed for max of {heal} health");
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
        
        if (SaveSystem.LoadPlayerHighScore() < GameStateManager.currentScore())
        {
            SaveSystem.SavePlayerHighScore(GameStateManager.currentScore());
        }
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void ChangeDamageState(DamageState state)
    {
        Debug.Log($"The player damage state has changed to {state}");
        this.state = state;
    }

    public DamageState getPlayerState ()
    {
        return state;
    }

    
}

public enum DamageState
{
    Normal, DoubledDamage, Immune
}

