using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MoveAlongPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public bool destroySelfAtEnd;
    public float speed = 10f;
    float distanceTravelled;

    // Update is called once per frame
    void Update()
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
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

        } else if (destroySelfAtEnd)
        {
            // Destroy self if the end of the path is reached
            gameObject.SetActive(false);
        }
    }
}
