using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellElement : MonoBehaviour
{
    public BulletHellApplication app
    {
        get { return GameObject.FindObjectOfType<BulletHellApplication>(); }
    }
}
