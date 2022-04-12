using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyControllerBase, IController, IObjectPool
{
    private BasicEnemyModel basicEnemyModel;
    private PlayerBulletModel playerBulletModel;

    private List<GameObject> pooledEnemyViews;
    private float[] enemyViewHealths;
    private GameObject objectToPool;
    private int amountToPool;

    private void Awake()
    {
        basicEnemyModel = app.modelContainer.basicEnemyModel;
        playerBulletModel = app.modelContainer.playerBulletModel;

        amountToPool = basicEnemyModel.amountToPool;
        objectToPool = basicEnemyModel.objectToPool;

        initializeObjectPool();
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
                    onCollisionWithPlayerBullet(p_data);
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

        enemyViewHealths = new float[amountToPool];
        for(int i = 0; i < amountToPool; i++)
        {
            enemyViewHealths[i] = basicEnemyModel.initialHealth;
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

    private void onCollisionWithPlayerBullet(params object[] p_data)
    {
        GameObject basicEnemyView = (GameObject)p_data[1];

        takeDamage(playerBulletModel.damageAmount, basicEnemyView);
    }

    private void takeDamage(int damageAmount, GameObject basicEnemyView)
    {
        int healthIndex = pooledEnemyViews.IndexOf(basicEnemyView);

        if (enemyViewHealths[healthIndex] <= 0)
        {
            resetEnemyObject(basicEnemyView);
            return;
        }
        enemyViewHealths[healthIndex] -= damageAmount;

        MeshRenderer basicEnemyViewRenderer = basicEnemyView.GetComponent<MeshRenderer>();

        if (basicEnemyViewRenderer != null)
        {
            Color originalColor = basicEnemyViewRenderer.material.color;
            FlashRed(basicEnemyViewRenderer, originalColor);
        }
    }

    private void FlashRed(MeshRenderer basicEnemyViewRenderer, Color originalColor)
    {
        if (basicEnemyViewRenderer.material.color != basicEnemyModel.flashColor)
        {
            basicEnemyViewRenderer.material.color = basicEnemyModel.flashColor;
            StartCoroutine(ResetColor(basicEnemyViewRenderer, originalColor, basicEnemyModel.flashTime));
        }
    }

    private IEnumerator ResetColor(MeshRenderer basicEnemyViewRenderer, Color originalColor, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        basicEnemyViewRenderer.material.color = originalColor; 
    }

    private void resetEnemyObject(GameObject basicEnemyView)
    {
        basicEnemyView.SetActive(false); 
        basicEnemyView.transform.position = Vector3.zero;

        int healthIndex = pooledEnemyViews.IndexOf(basicEnemyView);
        enemyViewHealths[healthIndex] = basicEnemyModel.initialHealth;
    }
}
