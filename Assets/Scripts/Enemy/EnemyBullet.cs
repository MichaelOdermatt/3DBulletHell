using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage = 25f;
    private float Speed = 15f;
    private float createdAtTime;
    private float destroyAfterTime = 6f;

    private void Start()
    {
        createdAtTime = Time.time;
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
        moveBullet();
        destroyAfter(createdAtTime, destroyAfterTime);
    }

    void moveBullet()
    {
        Vector3 direction = new Vector3(0, 0, -1);
        Vector3 currentPos = transform.position;
        transform.position = currentPos + direction * Speed * Time.deltaTime; 
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
