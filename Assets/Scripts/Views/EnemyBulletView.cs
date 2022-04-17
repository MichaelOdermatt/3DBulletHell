using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletView : BulletHellElement 
{
    private void OnBecameInvisible()
    {
        app.Notify(BulletHellNotification.EnemyBulletOnInvisible , this, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BulletHellNotification.EnemyBulletOnCollision , this, gameObject, collision); 
    }
}
