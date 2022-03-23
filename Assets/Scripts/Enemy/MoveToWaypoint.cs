using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoint : MonoBehaviour
{
    private Rigidbody rigidbody;

    public GameObject Waypoint;
    public Vector3 WaypointCoords;
    public float speed = 15;

    private float distanceThreshold = 0.25f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardWaypoint(Waypoint, speed);
    }

    private void MoveTowardWaypoint(GameObject Waypoint, float speed)
    {
        if (Waypoint == null)
        {
            return;
        }


        float distanceToDestination = Vector3.Distance(rigidbody.position, WaypointCoords);

        if ((distanceToDestination - speed * Time.deltaTime) > distanceThreshold)
        {
            Vector3 movementVector = (WaypointCoords - transform.position).normalized;
            Vector3 newPos = rigidbody.position += movementVector * speed * Time.deltaTime;

            rigidbody.MovePosition(newPos);
        }
    }

    public void ResetValues()
    {
        Waypoint = null;
        rigidbody.position = Vector3.zero;
    }

    public void setWaypoint(GameObject waypoint)
    {
        Waypoint = waypoint;
        WaypointCoords = Waypoint.transform.position;
    }
}
