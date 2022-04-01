using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletHellElement 
{

    // will be in charge of creating bullet pool and providing methods to access pooled bullets

    private List<GameObject> pooledBulletViews;
    private GameObject objectToPool;
    private int amountToPool;

    private void Awake()
    {
        objectToPool = app.viewBase.playerBulletView.gameObject;
        amountToPool = app.modelBase.playerBulletModel.bulletsToPool;
    }
}
