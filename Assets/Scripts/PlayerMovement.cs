using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;
    public GameObject meshGenObject;
    public Camera camera;
    public Animator animator;
    private MeshGenerator meshGen;
    public GameObject playerModel;
    private SkinnedMeshRenderer modelMeshRenderer;
    public float maxSpeed = 18f;
    private float speed = 0f;
    private float acceleration = 12f;
    private float decceleration = 12f;

    private float health = 100f;
    private float flashTime = 0.1f;
    private Color flashColor = Color.red;
    private Color originalColor;

    private Vector3 playerCenter = new Vector3(50, 25, 0);
    private float floatBackToCenterSpeed = 1.5f;
    private float mapScrollSpeed = 0.02f;

    private float maxVerticalPosition = 55f;
    private float minVerticalPosition = 20f;

    private float maxHorizontalPosition = 85f;
    private float minHorizontalPosition = 15f;

    private Vector3 playerDefaultRotation = new Vector3(0, 0, 0);
    private float rotationSpeed = 5.5f;
    private float rotateToDefaultSpeed = 1f;
    private float maxRotationAngleX = 25f;
    private float maxRotationAngleZ = 25f;

    private float cameraBobHeight = 0.75f;
    private float cameraBobSpeed = 1.5f;
    private float cameraBobTimer = 0f;
    private Vector3 cameraDefaultPosition;
    private Vector3 cameraDefaultRotation;
    private float cameraRotationSpeed = 1.2f;
    private float maxCameraRotationAngleX = 10f;
    private float maxCameraRotationAngleZ = 10f;

    private void Awake()
    {
        modelMeshRenderer = playerModel.GetComponent<SkinnedMeshRenderer>();
        originalColor = modelMeshRenderer.material.color;
    
        meshGen = meshGenObject.GetComponent<MeshGenerator>();
        cameraDefaultRotation = camera.transform.rotation.eulerAngles;
        cameraDefaultPosition = camera.transform.position;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // set player rotaton when an input is given
        Quaternion target = Quaternion.Euler(maxRotationAngleX * vertical * -1, 0, maxRotationAngleZ * horizontal * -1);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);

        // set camera XZ rotation when an input is given
        Quaternion cameraTarget = Quaternion.Euler(cameraDefaultRotation.x + maxCameraRotationAngleX * vertical * -1 , cameraDefaultRotation.y, maxCameraRotationAngleZ * horizontal * -1);
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, cameraTarget, cameraRotationSpeed * Time.deltaTime);

        // bob the camera up and down
        cameraBobTimer += cameraBobSpeed * Time.deltaTime;
        camera.transform.position = new Vector3(cameraDefaultPosition.x, cameraDefaultPosition.y + Mathf.Sin(cameraBobTimer) * cameraBobHeight, cameraDefaultPosition.z);

        // get player movement direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //accelerationAndDeceleration(horizontal, vertical, Time.deltaTime);

        moveWhileClamping(direction * maxSpeed * Time.deltaTime);

        // rotate back to origin rotation
        if (vertical == 0)
        {
            float step = rotateToDefaultSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(playerDefaultRotation), step);
        }

        // scroll the map when the player moves
        var threshold = 0.5;
        if (transform.position.x >= maxHorizontalPosition - threshold || transform.position.x <= minHorizontalPosition + threshold)
        {
            meshGen.offset.x += horizontal * mapScrollSpeed;
        }

        // send the variable to the animator
        animator.SetInteger("IsAccending", (int)vertical);

        // keep the player moving forward at all times
        //meshGen.offset.y += mapScrollSpeed;
        //meshGen.updateAll();
    }

    /// <summary>
    /// If your x or y position in the next frame is greater than the cap, set the movement value in that direction to zero. 
    /// </summary>
    private void moveWhileClamping(Vector3 movementVector)
    {
        float xPositionNextStep = transform.position.x + movementVector.x;
        float zPositionNextStep = transform.position.z + movementVector.z;

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

    /// <summary>
    /// Handles player acceleration and player decceleration, honestly doesn't work very well, should be remade
    /// </summary>
    private void accelerationAndDeceleration(float horizontal, float vertical, float deltaTime)
    {
        if ((vertical != 0 || horizontal != 0) && speed < maxSpeed)
        {
            speed = speed + acceleration * deltaTime;
        } else
        {
            if (speed > decceleration * deltaTime)
            {
                speed = speed - decceleration * deltaTime;
            } else
            {
                speed = 0;
            }
        }
    }

    private void flashRed()
    {
        modelMeshRenderer.material.color = flashColor;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        modelMeshRenderer.material.color = originalColor; 
    }

    public void takeDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
            flashRed();
        } else
        {
            Destroy(gameObject);
        }
    }
}
