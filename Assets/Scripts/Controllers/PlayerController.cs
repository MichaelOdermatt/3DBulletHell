using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BulletHellElement, IController
{
    private PlayerModel playerModel;
    private PlayerBulletController playerBulletController;

    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private CharacterController characterController;

    private void Awake()
    {
        playerModel = app.modelContainer.playerModel;
        playerBulletController = app.controllerContainer.playerBulletController;
        characterController = app.viewContainer.playerView.characterController;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            playerBulletController.CreateBulletAtTransform(characterController.transform);
        }
    }

    private void FixedUpdate()
    {
        // get player movement direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        // set player rotaton when an input is given
        Quaternion target = Quaternion.Euler(0, 0, playerModel.maxRotationAngleZ * horizontal * -1);
        Quaternion currentRotation = characterController.transform.rotation;
        characterController.transform.rotation = Quaternion.Slerp(
            currentRotation, 
            target, 
            playerModel.rotationSpeed * Time.deltaTime);

        moveWhileClamping(direction * playerModel.maxSpeed * Time.deltaTime);
    }

    /// <summary>
    /// If your x or y position in the next frame is greater than the cap, set the 
    /// movement value in that direction to zero. 
    /// </summary>
    private void moveWhileClamping(Vector3 movementVector)
    {
        float xPositionCurrentStep = characterController.transform.position.x;
        float zPositionCurrentStep = characterController.transform.position.z;

        float xPositionNextStep = xPositionCurrentStep + movementVector.x;
        float zPositionNextStep = zPositionCurrentStep + movementVector.z;

        if (zPositionNextStep > playerModel.maxVerticalPosition || zPositionNextStep < playerModel.minVerticalPosition)
        {
            movementVector.z = 0;
        } 

        if (xPositionNextStep > playerModel.maxHorizontalPosition || xPositionNextStep < playerModel.minHorizontalPosition)
        {
            movementVector.x = 0;
        }

        characterController.Move(movementVector);
    }

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
    }
}
