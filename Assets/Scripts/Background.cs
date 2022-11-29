using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float bgImageConstant = 2.5577f;
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
        ReSetPoint = new Vector3((transform.position.x - GetBGWidth()), 0f);
        //getBGSize();
        //Debug.Log(ReSetPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(moveSpeed * GameStateManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public float GetBGWidth()
    {
        //float bg = new Vector3 ()
        int num = 0;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].CompareTag( "Backgrounds"))
            {
                num++;
            }
        }
        //Debug.Log(num);
        return transform.localScale.x * num * bgImageConstant;
    }
}
