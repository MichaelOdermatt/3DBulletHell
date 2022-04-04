using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BulletHellElement 
{

    private CameraModel cameraModel;
    private CameraView cameraView;

    private float horizontal;
    private float vertical;

    private float cameraBobHeight;
    private float cameraBobSpeed;
    private float cameraBobTimer;
    private Vector3 cameraDefaultPosition;
    private Vector3 cameraDefaultRotation;
    private float cameraRotationSpeed;
    private float maxCameraRotationAngleX;
    private float maxCameraRotationAngleZ;

    private void Awake()
    {
        cameraModel = app.modelContainer.cameraModel;
        cameraView = app.viewContainer.cameraView;

        cameraBobHeight = cameraModel.cameraBobHeight;
        cameraBobSpeed = cameraModel.cameraBobSpeed;
        cameraBobTimer = cameraModel.cameraBobTimer;
        cameraDefaultPosition = app.viewContainer.cameraView.transform.position;
        cameraDefaultRotation = app.viewContainer.cameraView.transform.rotation.eulerAngles;
        cameraRotationSpeed = cameraModel.cameraRotationSpeed;
        maxCameraRotationAngleX = cameraModel.maxCameraRotationAngleX;
        maxCameraRotationAngleZ = cameraModel.maxCameraRotationAngleZ;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // set camera XZ rotation when an input is given
        Quaternion cameraTarget = Quaternion.Euler(cameraDefaultRotation.x + maxCameraRotationAngleX * vertical * -1 , 
            cameraDefaultRotation.y, 
            maxCameraRotationAngleZ * horizontal * -1);

        cameraView.transform.rotation = Quaternion.Slerp(cameraView.transform.rotation, 
            cameraTarget, 
            cameraRotationSpeed * Time.deltaTime);

        // bob the camera up and down
        cameraBobTimer += cameraBobSpeed * Time.deltaTime;
        cameraView.transform.position = new Vector3(cameraDefaultPosition.x, 
            cameraDefaultPosition.y + Mathf.Sin(cameraBobTimer) * cameraBobHeight, 
            cameraDefaultPosition.z);
    }
}
