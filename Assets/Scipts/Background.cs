using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float MoveSpeed = 15f;

    public float moveSpeed { get; private set; }

    [SerializeField]
    private int lengthMin, lengthMax;

    [SerializeField]
    private Vector2 bgDimentions, bgSpawn;

    public Vector2 dimentions { get; private set; }
    public int repeats { get; private set; } 

    public Vector3 ReSetPoint { get; private set; }



    void Awake()
    {
        moveSpeed = MoveSpeed;
        repeats = Random.Range(lengthMin, lengthMax);
        //transform.position = new Vector3(bgSpawn.x, bgSpawn.y, 0.1f);
        ReSetPoint = new Vector3(-(bgDimentions.x - bgSpawn.x), bgSpawn.y);
        Debug.Log(ReSetPoint);
        dimentions = bgDimentions;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= new Vector3(MoveSpeed * Time.deltaTime, 0f, 0f);
    }

}
