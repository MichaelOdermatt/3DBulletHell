using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BaseBulletController 
{
    private EnemyBulletModel enemyBulletModel;

    private void Awake()
    {
        enemyBulletModel = app.modelContainer.enemyBulletModel;

        objectToPool = enemyBulletModel.objectToPool;
        amountToPool = enemyBulletModel.bulletsToPool;

        initializeObjectPool();
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
    }
}
