using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    public delegate void Fire(GameObject sender);
    public static Fire fire;

    public GameObject objectToShoot;
    private float shootDelay = 1.5f;
    private float lastShootTime;

    // Update is called once per frame
    void Update()
    {
        if (lastShootTime + shootDelay > Time.time)
        {
            return;
        }
        lastShootTime = Time.time;

        // Fire the delegate function for the Enemy Manager.
        fire(gameObject);

    }
}
