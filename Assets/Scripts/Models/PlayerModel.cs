using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BulletHellElement 
{
    public float maxSpeed = 18f;

    public float health = 100f;
    public readonly float flashTime = 0.1f;
    public readonly Color flashColor = Color.red;
    public readonly Color originalColor;

    public readonly float maxVerticalPosition = 55f;
    public readonly float minVerticalPosition = 20f;

    public readonly float maxHorizontalPosition = 85f;
    public readonly float minHorizontalPosition = 15f;

    public readonly Vector3 playerDefaultRotation = new Vector3(0, 0, 0);
    public readonly float rotationSpeed = 5.5f;
    public readonly float rotateToDefaultSpeed = 1f;
    public readonly float maxRotationAngleX = 25f;
    public readonly float maxRotationAngleZ = 25f;

}
