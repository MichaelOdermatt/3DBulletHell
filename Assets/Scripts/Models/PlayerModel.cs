using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BulletHellElement 
{

    public float maxSpeed = 18f;

    private float health = 100f;
    private float flashTime = 0.1f;
    private Color flashColor = Color.red;
    private Color originalColor;

    private float maxVerticalPosition = 55f;
    private float minVerticalPosition = 20f;

    private float maxHorizontalPosition = 85f;
    private float minHorizontalPosition = 15f;

    private Vector3 playerDefaultRotation = new Vector3(0, 0, 0);
    private float rotationSpeed = 5.5f;
    private float rotateToDefaultSpeed = 1f;
    private float maxRotationAngleX = 25f;
    private float maxRotationAngleZ = 25f;

}
