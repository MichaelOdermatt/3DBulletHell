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
            playerBulletController.CreateBulletAtPosition(characterController.transform.position);
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
        switch (p_event_path)
        {
            case BulletHellNotification.PlayerControllerOnCollision:
                Collision collision = (Collision)p_data[0];
                GameObject collider = collision.collider.gameObject;

                if (collider.GetComponent<EnemyBulletView>())
                {
                    takeDamage(app.modelContainer.enemyBulletModel.damageAmount);
                }

                break;
        }
    }

    private void takeDamage(int damageAmount)
    {
        playerModel.health -= damageAmount;

        SkinnedMeshRenderer playerMeshRenderer = app.
            viewContainer.
            playerView.
            characterModel.
            GetComponent<SkinnedMeshRenderer>();

        if (playerMeshRenderer != null)
        {
            Color originalColor = playerMeshRenderer.material.color;
            FlashRed(playerMeshRenderer, originalColor);
        }
    }

    private void FlashRed(SkinnedMeshRenderer meshRenderer, Color originalColor)
    {
        if (meshRenderer.material.color != playerModel.flashColor)
        {
            meshRenderer.material.color = playerModel.flashColor;
            StartCoroutine(ResetColor(meshRenderer, originalColor, playerModel.flashTime));
        }
    }

    private IEnumerator ResetColor(SkinnedMeshRenderer meshRenderer, Color originalColor, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        meshRenderer.material.color = originalColor; 
    }
}
