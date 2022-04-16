using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BaseBulletController, IController, IObjectPool
{

    private PlayerBulletModel playerBulletModel;

    private void Awake()
    {
        playerBulletModel = app.modelContainer.playerBulletModel;

        objectToPool = playerBulletModel.objectToPool;
        amountToPool = playerBulletModel.bulletsToPool;

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
            bulletRigidBody.velocity = playerBulletModel.bulletVelocity;
        }
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case BulletHellNotification.PlayerBulletOnInvisible:
            case BulletHellNotification.PlayerBulletOnCollision:
                GameObject gameObject = (GameObject)p_data[0];

                deactivateBulletInstance(gameObject);
                break;
        }
    }
}
