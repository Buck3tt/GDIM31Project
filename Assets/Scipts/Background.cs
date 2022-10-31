using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour, MovementObject
{
    // Start is called before the first frame update
    [SerializeField]
    private float speedMultiplyer = 0f;

    public float speedMult { get; private set; }

    [SerializeField]
    private int lengthMin, lengthMax;

    public int repeats { get; private set; } 

    public Vector3 ReSetPoint { get; private set; }

    private float movespeed = 0f;


    void Awake()
    {
        speedMult = speedMultiplyer;
        repeats = Random.Range(lengthMin, lengthMax);
        //transform.position = new Vector3(bgSpawn.x, bgSpawn.y, 0.1f);
        ReSetPoint = new Vector3(-(transform.localScale.x - transform.position.x), transform.position.y);
        Debug.Log(ReSetPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
        if(transform.position.x <= ReSetPoint.x)
        {
            FindObjectOfType<GameStateManager>().nextBG();
        }
    }

    void MovementObject.SetMovement(float movement)
    {
        movespeed = movement;
    }
}
