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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rb.position += new Vector2(Input.GetAxis("Horizontal") * movementForce, Input.GetAxis("Vertical") * movementForce);
    }

    public void Death()
    {
        //GameStateManager.currentScore()
        
        if (SaveSystem.LoadPlayerHighScore() < BackgroundSpawner.currentScore())
        {
            SaveSystem.SavePlayerHighScore(BackgroundSpawner.currentScore());
        }
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
