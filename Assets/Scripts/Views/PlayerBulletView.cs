using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletView : BulletHellElement 
{
    private void OnBecameInvisible()
    {
        Debug.Log("Destroy me"); 
    }
}
