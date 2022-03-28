using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BulletHellElement 
{
    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private float maxSpeed;

    private CharacterController characterController;

    private float maxVerticalPosition;
    private float minVerticalPosition;
    private float maxHorizontalPosition;
    private float minHorizontalPosition;

    private void Awake()
    {
        maxSpeed = app.modelBase.playerModel.maxSpeed;

        maxVerticalPosition = app.modelBase.playerModel.maxVerticalPosition;
        minVerticalPosition = app.modelBase.playerModel.minVerticalPosition;
        maxHorizontalPosition = app.modelBase.playerModel.maxHorizontalPosition;
        minHorizontalPosition = app.modelBase.playerModel.minHorizontalPosition;

        characterController = app.playerView.characterController;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // get player movement direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        moveWhileClamping(direction * maxSpeed * Time.deltaTime);
    }

    /// <summary>
    /// If your x or y position in the next frame is greater than the cap, set the movement value in that direction to zero. 
    /// </summary>
    private void moveWhileClamping(Vector3 movementVector)
    {
        float xPositionCurrentStep = app.playerView.characterController.transform.position.x;
        float zPositionCurrentStep = app.playerView.characterController.transform.position.z;

        float xPositionNextStep = xPositionCurrentStep + movementVector.x;
        float zPositionNextStep = zPositionCurrentStep + movementVector.z;

        if (zPositionNextStep > maxVerticalPosition || zPositionNextStep < minVerticalPosition)
        {
            movementVector.z = 0;
        } 

        if (xPositionNextStep > maxHorizontalPosition || xPositionNextStep < minHorizontalPosition)
        {
            movementVector.x = 0;
        }

        characterController.Move(movementVector);
    }

}
