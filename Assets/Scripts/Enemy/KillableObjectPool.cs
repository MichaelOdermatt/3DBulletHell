using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableObjectPool : Killable 
{
    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
