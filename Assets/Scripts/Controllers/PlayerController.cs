using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BulletHellElement 
{
    private PlayerModel playerModel;

    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private float maxSpeed;

    private CharacterController characterController;

    private float maxVerticalPosition;
    private float minVerticalPosition;
    private float maxHorizontalPosition;
    private float minHorizontalPosition;

    private float rotationSpeed;
    private float maxRotationAngleZ;

    private void Awake()
    {
        playerModel = app.modelBase.playerModel;

        maxVerticalPosition = playerModel.maxVerticalPosition;
        minVerticalPosition = playerModel.minVerticalPosition;
        maxHorizontalPosition = playerModel.maxHorizontalPosition;
        minHorizontalPosition = playerModel.minHorizontalPosition;

        maxSpeed = playerModel.maxSpeed;
        rotationSpeed = playerModel.rotationSpeed;
        maxRotationAngleZ = playerModel.maxRotationAngleZ;

        characterController = app.viewBase.playerView.characterController;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // get player movement direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        // set player rotaton when an input is given
        Quaternion target = Quaternion.Euler(0, 0, maxRotationAngleZ * horizontal * -1);
        Quaternion currentRotation = characterController.transform.rotation;
        characterController.transform.rotation = Quaternion.Slerp(currentRotation, target, rotationSpeed * Time.deltaTime);

        moveWhileClamping(direction * maxSpeed * Time.deltaTime);
    }

    /// <summary>
    /// If your x or y position in the next frame is greater than the cap, set the movement value in that direction to zero. 
    /// </summary>
    private void moveWhileClamping(Vector3 movementVector)
    {
        float xPositionCurrentStep = characterController.transform.position.x;
        float zPositionCurrentStep = characterController.transform.position.z;

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
