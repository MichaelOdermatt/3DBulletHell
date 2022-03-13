using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableEnemy : Killable
{
    public delegate void EnemyKilled();
    public static EnemyKilled enemyKilled;

    public override void Die()
    {
        gameObject.SetActive(false);

        // reset the game objects health
        this.ResetValues();

        // reset the enemy's moveAlongPath Component
        MoveAlongPath moveAlongPath = gameObject.GetComponent<MoveAlongPath>();
        if (moveAlongPath != null)
        {
            moveAlongPath.ResetValues();
        }

        // reset the enemy's moveToWaypoint Component
        MoveToWaypoint moveToWaypoint = gameObject.GetComponent<MoveToWaypoint>();
        if (moveToWaypoint != null)
        {
            moveAlongPath.ResetValues();
        }

        // fire the deleget to notify the enemy manager
        enemyKilled();
    }
}
