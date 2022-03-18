using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoint : MonoBehaviour
{
    private Rigidbody rigidbody;

    public GameObject Waypoint;
    public float speed = 15;

    private float distanceThreshold = 0.25f;

    // Start is called before the first frame update
    void Start()
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


        Vector3 waypointCoords = Waypoint.transform.position;
        float distanceToDestination = Vector3.Distance(rigidbody.position, waypointCoords);

        if ((distanceToDestination - speed * Time.deltaTime) > distanceThreshold)
        {
            Vector3 movementVector = (waypointCoords - transform.position).normalized;
            Vector3 newPos = rigidbody.position += movementVector * speed * Time.deltaTime;

            rigidbody.MovePosition(newPos);
        }
    }

    public void ResetValues()
    {
        Waypoint = null;
        rigidbody.position = Vector3.zero;
    }
}
