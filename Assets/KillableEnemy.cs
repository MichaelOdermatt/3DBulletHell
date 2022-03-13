using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableEnemy : Killable
{
    public override void Die()
    {
        gameObject.SetActive(false);

        // reset the game objects health
        actualHealth = initialHealth;

        MoveAlongPath moveAlongPath = gameObject.GetComponent<MoveAlongPath>();
        if (moveAlongPath != null)
        {
            moveAlongPath.pathCreator = null;
            moveAlongPath.distanceTravelled = 0f;
        }

        MoveToWaypoint moveToWaypoint = gameObject.GetComponent<MoveToWaypoint>();
        if (moveToWaypoint != null)
        {
            moveToWaypoint.Waypoint = null;
        }
    }
}
