using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float movementForce;

    [SerializeField]
    private float maxHealth = 10f;

    private float currentHealth;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float playerRotationAmmount = 4.5f;

    [SerializeField]
    private GameObject heartPreFab;
    [SerializeField]
    private Transform heartLocation;
    [SerializeField]
    private int HearDivisor = 10;

    private List<Image> hearts;

    private float heartWidth;

    //private List<Bonus>
    public enum DamageState
    {
        Normal, DoubledDamage, Immune
    }
    [SerializeField]
    private DamageState state = DamageState.Normal;

    private Animator animator;

    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        hearts = new List<Image>();

        heartWidth = heartPreFab.GetComponent<Image>().rectTransform.rect.width * 5f;

        Debug.Log((int)maxHealth /  HearDivisor);

        //Instantiate(heartPreFab, new Vector3(0f, 0f), Quaternion.identity, heartLocation);
        //Instantiate(heartPreFab, heartLocation, false);

        for (int i = 0; i < (int)maxHealth/ HearDivisor; i++)
        {
            //hearts.Add(Instantiate(heartPreFab, heartLocation.position + new Vector3(i * heartPreFab.GetComponent<Image>().flexibleWidth, 0f), Quaternion.identity, heartLocation).GetComponent<Image>());
            hearts.Add(Instantiate(heartPreFab, heartLocation, false).GetComponent<Image>());
            hearts[i].transform.localPosition = new Vector3(i * heartWidth, 0, 0);
            hearts[i].name = $"Heart {i}";
        }
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
        animator.SetBool("Moving", Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
        animator.speed = GameStateManager.msMult;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        //binds movement within camera frame, numbers could use some adjusting to be more exact
        float minY = -mainCamera.orthographicSize + gameObject.transform.localScale.y;
        float maxY = minY * -1;
        float minX = -(mainCamera.orthographicSize * mainCamera.aspect) + gameObject.transform.localScale.x;
        float maxX = minX * -1;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, minX, maxX), Mathf.Clamp(rb.position.y, minY, maxY));

        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Input.GetAxisRaw("Vertical") * playerRotationAmmount);
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

            FindObjectOfType<AudioManager>().Play("PlayerDeath");
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
        FindObjectOfType<AudioManager>().Play("PlayerHeal");
    }

    private void UpdateHealth()
    {
        //hp.fillAmount = currentHealth / maxHealth;

        if (hearts.Count - 1 > (int)currentHealth /  HearDivisor)
            for (int i = hearts.Count - 1; i >= 0; i--)
            {
                if (i > (int)currentHealth /  HearDivisor) {
                    Destroy(hearts[i]);
                    hearts.RemoveAt(i);
                }
                else
                {
                    hearts[i].fillAmount = 1f;
                }
            }
        else if (hearts.Count - 1 < (int)currentHealth /  HearDivisor)
        {
            
            for (int i = 0; i < (int)currentHealth /  HearDivisor; i++)
            {
                try
                {
                    hearts[i].fillAmount = 1f;
                }
                catch
                {
                    hearts.Add(Instantiate(heartPreFab, heartLocation, false).GetComponent<Image>());
                    hearts[i].transform.localPosition = new Vector3(i * heartWidth, 0, 0);
                    hearts[i].name = $"Heart {i}";
                }
            }
        }
        try { hearts[(int)currentHealth / HearDivisor].fillAmount = (float) (currentHealth % HearDivisor) / HearDivisor; }
        catch { hearts[hearts.Count - 1].fillAmount = 1f;  }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Powerup"))
        {
            collision.gameObject.GetComponent<PowerUp>().usepowerup();
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }
    public void Death()
    {
        PlayerData data = new PlayerData(GameStateManager.GetScore());
        SaveSystem.SavePlayerHighScore(data);
        GameStateManager.GameOver();
    }

    public void ChangeDamageState(DamageState state)
    {
        Debug.Log($"The player damage state has changed to {state}");
        this.state = state;
        switch (state)
        {
            case DamageState.Normal:
                foreach(Image hp in hearts)
                    hp.color = new Color(255, 255, 255, 1);
                break;
            case DamageState.DoubledDamage:
                break;
            case DamageState.Immune:
                foreach (Image hp in hearts)
                    hp.color = new Color(255, 255, 0, 1);
                break;
        }
    }

    public DamageState GetPlayerState()
    {
        return state;
    }

    public List<Image> GetHealthImage()
    {
        return hearts;
    }
}

