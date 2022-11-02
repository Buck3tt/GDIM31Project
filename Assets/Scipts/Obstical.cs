using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstical : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<Player>().Death();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
