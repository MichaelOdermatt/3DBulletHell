using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletModel : BulletHellElement 
{
    public int bulletsToPool = 50;
    public int damageAmount = 15;
    public Vector3 bulletVelocity = new Vector3(0, 0, 20);

    public GameObject objectToPool;
}
