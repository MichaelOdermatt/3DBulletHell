using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BulletHellElement 
{
    public float maxSpeed = 18f;

    public float health = 100f;
    public float flashTime = 0.1f;
    public Color flashColor = Color.red;
    public Color originalColor;

    public float maxVerticalPosition = 55f;
    public float minVerticalPosition = 20f;

    public float maxHorizontalPosition = 85f;
    public float minHorizontalPosition = 15f;

    public Vector3 playerDefaultRotation = new Vector3(0, 0, 0);
    public float rotationSpeed = 5.5f;
    public float rotateToDefaultSpeed = 1f;
    public float maxRotationAngleX = 25f;
    public float maxRotationAngleZ = 25f;

}
