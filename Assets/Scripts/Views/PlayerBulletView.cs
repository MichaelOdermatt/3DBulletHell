using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletView : BulletHellElement 
{
    private void OnBecameInvisible()
    {
        app.Notify(BulletHellNotification.PlayerBulletOnInvisible, this, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BulletHellNotification.PlayerBulletOnCollision, this, gameObject, collision); 
    }
}
