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

    // Start is called before the first frame update
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
        currentHealth = Mathf.Clamp(currentHealth-damage, 0, maxHealth);

        if (currentHealth == 0f)
        {
            Death();
        }
        else
        {
            hp.fillAmount = currentHealth / maxHealth;
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
}
