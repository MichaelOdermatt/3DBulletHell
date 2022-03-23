using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MoveAlongPath : MonoBehaviour
{
    private Rigidbody rigidbody;

    public PathCreator pathCreator;
    public bool destroySelfAtEnd;
    public float speed = 10f;
    public float distanceTravelled;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveAlongPath(speed);
    }

    private void moveAlongPath(float speed)
    {
        if (pathCreator == null)
        {
            return;
        }

        if (distanceTravelled < (pathCreator.path.length - speed * Time.deltaTime))
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 newPos = pathCreator.path.GetPointAtDistance(distanceTravelled);
            rigidbody.MovePosition(newPos); 

        } else if (destroySelfAtEnd)
        {
            // Destroy self if the end of the path is reached
            Killable killable = gameObject.GetComponent<Killable>();
            if (killable != null)
            {
                killable.Die();
            } 
        }
    }

    public void ResetValues()
    {
        pathCreator = null;
        distanceTravelled = 0f;
        rigidbody.position = Vector3.zero;
    }
}
