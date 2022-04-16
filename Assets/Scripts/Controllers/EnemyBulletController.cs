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

    public override void CreateBulletAtPosition(Vector3 position)
    {
        GameObject BulletInstance = GetPooledObject();
        if (BulletInstance == null)
        {
            return;
        }

        // since the physics update cycles stop firing when an object is deactivated
        // set the position of the game object rather than on its rigidbody.
        BulletInstance.transform.position = position;
        BulletInstance.SetActive(true);

        Rigidbody bulletRigidBody = BulletInstance.GetComponent<Rigidbody>();
        if (bulletRigidBody != null)
        {
            bulletRigidBody.velocity = enemyBulletModel.bulletVelocity;
        }

    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case BulletHellNotification.EnemyBulletOnInvisible:
                GameObject gameObject = (GameObject)p_data[0];

                deactivateBulletInstance(gameObject);
                break;
        }
    }
}
