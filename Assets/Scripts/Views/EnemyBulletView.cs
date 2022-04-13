using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletView : BulletHellElement 
{
    private void OnBecameInvisible()
    {
        app.Notify(BulletHellNotification.PlayerBulletOnInvisible, this, gameObject);
    }
}
