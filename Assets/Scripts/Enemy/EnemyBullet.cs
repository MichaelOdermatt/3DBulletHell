using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float damage = 25f;
    private float Speed = 15f;
    Vector3 direction = new Vector3(0, 0, -1);
    private float createdAtTime;
    private float destroyAfterTime = 6f;

    private void Start()
    {
        createdAtTime = Time.time;
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Whenever the bullet is set to active again, reset the createdAtTime.
    /// </summary>
    private void OnEnable()
    {
        createdAtTime = Time.time;
    }

    void Update()
    {
        destroyAfter(createdAtTime, destroyAfterTime);
    }

    private void FixedUpdate()
    {
        moveBullet(); 
    }

    void moveBullet()
    {
        Vector3 currentPos = rigidBody.position;
        Vector3 newPos = currentPos + direction * Speed * Time.deltaTime;
        rigidBody.MovePosition(newPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.takeDamage(damage);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void destroyAfter(float createdAtTime, float destroyAfterTime)
    {
        if (createdAtTime + destroyAfterTime < Time.time)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
