using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletHellElement, IController, IObjectPool
{

    private PlayerBulletModel playerBulletModel;

    private List<GameObject> pooledBulletViews;
    private GameObject objectToPool;
    private int amountToPool;

    private void Awake()
    {
        playerBulletModel = app.modelContainer.playerBulletModel;

        objectToPool = playerBulletModel.objectToPool;
        amountToPool = playerBulletModel.bulletsToPool;

        initializeObjectPool();
    }

    private void initializeObjectPool() 
    { 
        pooledBulletViews = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledBulletViews.Add(tmp);
        }
    }

    public void CreateBulletAtPosition(Vector3 position)
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
        if (bulletRigidBody == null)
        {
            return;
        }

        bulletRigidBody.velocity = playerBulletModel.bulletVelocity;
    }

    private void deactivateBulletInstance(GameObject bulletInstance)
    {
        bulletInstance.SetActive(false);
        bulletInstance.transform.position = Vector3.zero;
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledBulletViews[i].activeInHierarchy)
            {
                return pooledBulletViews[i];
            }
        }
        return null;
    }

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
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
