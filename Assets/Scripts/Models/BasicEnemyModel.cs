using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyModel : EnemyModelBase 
{
    public float Health = 100f;
    public Color flashColor = Color.red;
    public Color originalColor;
    public float flashTime = 0.1f;
}
