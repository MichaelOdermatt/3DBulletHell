using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletHellElement, IController
{

    // will be in charge of creating bullet pool and providing methods to access pooled bullets

    private PlayerBulletModel playerBulletModel;

    private List<GameObject> pooledBulletViews;
    private GameObject objectToPool;
    private int amountToPool;

    private void Awake()
    {
        playerBulletModel = app.modelContainer.playerBulletModel; 

        objectToPool = app.viewContainer.playerBulletView.gameObject;
        amountToPool = app.modelContainer.playerBulletModel.bulletsToPool;

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

    public void CreateBulletAtTransform(Transform transform)
    {
        GameObject BulletInstance = GetPooledObject();
        if (BulletInstance == null)
        {
            return;
        }

        Rigidbody bulletRigidBody = BulletInstance.GetComponent<Rigidbody>();
        if (bulletRigidBody == null)
        {
            return;
        }

        BulletInstance.SetActive(true);
        bulletRigidBody.velocity = playerBulletModel.bulletVelocity;
        bulletRigidBody.position = transform.position; 
    }

    private void deactivateBulletInstance(GameObject bulletInstance)
    {
        bulletInstance.SetActive(false);

        Rigidbody bulletRigidBody = bulletInstance.GetComponent<Rigidbody>();
        if (bulletRigidBody == null)
        {
            return;
        }

        bulletRigidBody.velocity = Vector3.zero;
        bulletRigidBody.position = Vector3.zero;
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
