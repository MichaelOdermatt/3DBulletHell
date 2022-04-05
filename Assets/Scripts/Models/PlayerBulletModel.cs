using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletModel : BulletHellElement 
{
    public int bulletsToPool = 20;
    public int damageAmount = 25;
    public Vector3 bulletVelocity = new Vector3(0, 0, 20);
}
