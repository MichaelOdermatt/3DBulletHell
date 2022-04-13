using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyView : BulletHellElement 
{
    private double lastShootTime;

    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BulletHellNotification.BasicEnemyOnCollision, this, collision, this.gameObject);   
    }

    void Update()
    {
        if (lastShootTime + app.modelContainer.basicEnemyModel.fireRate > Time.time)
        {
            return;
        }
        lastShootTime = Time.time;

        app.Notify(BulletHellNotification.BasicEnemyOnFire, this, gameObject);
    }
}
