using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyControllerBase, IController
{
    private BasicEnemyModel model;

    private void Awake()
    {
        model = app.modelContainer.basicEnemyModel;
    }

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case BulletHellNotification.BasicEnemyOnCollision:
                Collision collision = (Collision)p_data[0];
                GameObject collider = collision.collider.gameObject;

                if (collider.GetComponent<PlayerBulletView>())
                {
                    // need to get damage ammount from playerBulletModel somehow
                    takeDamage(1);
                }

                break;
        }
    }

    private void takeDamage(int damageAmount)
    {
        model.Health -= damageAmount;
    }
}
