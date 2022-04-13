using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyModel : EnemyModelBase 
{
    public float initialHealth = 100f;
    public Color flashColor = Color.red;
    public Color originalColor;
    public float flashTime = 0.1f;
    public double fireRate = 1.5f;

    public int amountToPool = 20;
    public GameObject objectToPool;
}
