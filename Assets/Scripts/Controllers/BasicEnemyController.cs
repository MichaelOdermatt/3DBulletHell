using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyControllerBase, IController
{
    private BasicEnemyModel basicEnemyModel;
    private PlayerBulletModel playerBulletModel;

    private void Awake()
    {
        basicEnemyModel = app.modelContainer.basicEnemyModel;
        playerBulletModel = app.modelContainer.playerBulletModel;
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
                    takeDamage(playerBulletModel.damageAmount);
                }

                break;
        }
    }

    private void takeDamage(int damageAmount)
    {
        basicEnemyModel.Health -= damageAmount;
    }
}
