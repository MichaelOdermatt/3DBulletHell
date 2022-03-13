using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoint : MonoBehaviour
{
    public GameObject Waypoint;
    public float speed = 15;

    private float distanceThreshold = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardWaypoint(Waypoint, speed);
    }

    private void MoveTowardWaypoint(GameObject Waypoint, float speed)
    {
        if (Waypoint == null)
        {
            return;
        }


        Vector3 waypointCoords = Waypoint.transform.position;
        float distanceToDestination = Vector3.Distance(transform.position, waypointCoords);

        if ((distanceToDestination - speed * Time.deltaTime) > distanceThreshold)
        {
            Debug.Log("shouldbemoving");

            Vector3 movementVector = (waypointCoords - transform.position).normalized;

            transform.position += movementVector * speed * Time.deltaTime;
        }
    }
}
