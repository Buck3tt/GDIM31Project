using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float movementForce;

    private Vector3 toApplyMove;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            toApplyMove += new Vector3(-movementForce, 0, 0);
        }

        if(Input.GetKey(KeyCode.W))
        {
            toApplyMove += new Vector3(0, movementForce, 0);
        }

        if(Input.GetKey(KeyCode.D))
        {
            toApplyMove += new Vector3(movementForce, 0, 0);
        }

        if(Input.GetKey(KeyCode.S))
        {
            toApplyMove += new Vector3(0, -movementForce, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(toApplyMove);
        toApplyMove = Vector3.zero;
    }
}
