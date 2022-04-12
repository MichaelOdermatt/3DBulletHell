using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBulletController : BulletHellElement, IObjectPool, IController 
{

    protected List<GameObject> pooledBulletViews;
    protected GameObject objectToPool;
    protected int amountToPool;

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

    protected void initializeObjectPool() 
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

    public abstract void OnNotification(string p_event_path, Object p_target, params object[] p_data);
}
