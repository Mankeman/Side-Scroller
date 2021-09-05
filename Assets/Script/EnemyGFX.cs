using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath pathfinder;
    void Update()
    {
        if (pathfinder.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-8f, 8f, 1f);
        }
        else if (pathfinder.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(8f, 8f, 1f);
        }
        
    }
}
