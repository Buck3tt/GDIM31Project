using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed = 10f;

    //public float speedMult { get; private set; }

    //[SerializeField]
    //private int lengthMin, lengthMax;

    //public int repeats { get; private set; } 

    public Vector3 ReSetPoint { get; private set; }



    void Awake()
    {
        //speedMult = speedMultiplyer;
        //repeats = Random.Range(lengthMin, lengthMax);
        //transform.position = new Vector3(bgSpawn.x, bgSpawn.y, 0.1f);
        ReSetPoint = new Vector3((transform.position.x - transform.localScale.x), transform.position.y);
        //Debug.Log(ReSetPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(moveSpeed * GameSpawnnerManager.msMult * Time.deltaTime, 0f, 0f);
    }

}
