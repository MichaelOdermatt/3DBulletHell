using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyView : BulletHellElement 
{

    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BulletHellNotification.BasicEnemyOnCollision, this, collision);   
    }
}
