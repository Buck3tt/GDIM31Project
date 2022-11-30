using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float bgImageConstant = 2.5577f;

    public Vector3 ReSetPoint { get; private set; }



    void Awake()
    {
        ReSetPoint = new Vector3((transform.position.x - GetBGWidth()), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(moveSpeed * GameStateManager.msMult * Time.deltaTime, 0f, 0f);
    }

    public float GetBGWidth()
    {
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
