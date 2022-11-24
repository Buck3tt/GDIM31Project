using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyBase
{
    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(moveSpeed * GameStateManager.msMult * Time.deltaTime, 0f, 0f);
    }
}
