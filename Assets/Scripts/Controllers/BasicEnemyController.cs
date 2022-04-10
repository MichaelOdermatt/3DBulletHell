using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyControllerBase, IController, IObjectPool
{
    private BasicEnemyModel basicEnemyModel;
    private PlayerBulletModel playerBulletModel;
    private Renderer basicEnemyRenderer;

    private List<GameObject> pooledEnemyViews;
    private GameObject objectToPool;
    private int amountToPool;

    private void Awake()
    {
        basicEnemyModel = app.modelContainer.basicEnemyModel;
        playerBulletModel = app.modelContainer.playerBulletModel;

        basicEnemyRenderer = app.viewContainer.basicEnemyView.GetComponent<Renderer>();
        basicEnemyModel.originalColor = basicEnemyRenderer.material.color;

        amountToPool = basicEnemyModel.amountToPool;
        objectToPool = basicEnemyModel.objectToPool;
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

    private void initializeObjectPool() 
    { 
        pooledEnemyViews = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledEnemyViews.Add(tmp);
        }
    }


    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledEnemyViews[i].activeInHierarchy)
            {
                return pooledEnemyViews[i];
            }
        }
        return null;
    }

    private void takeDamage(int damageAmount)
    {
        basicEnemyModel.Health -= damageAmount;
        FlashRed();
    }

    private void FlashRed()
    {
        basicEnemyRenderer.material.color = basicEnemyModel.flashColor;
        Invoke("ResetColor", basicEnemyModel.flashTime);
    }

    private void ResetColor()
    {
        basicEnemyRenderer.material.color = basicEnemyModel.originalColor; 
    }
}
