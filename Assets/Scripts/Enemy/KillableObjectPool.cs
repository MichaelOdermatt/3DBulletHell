using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableObjectPool : Killable 
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}
