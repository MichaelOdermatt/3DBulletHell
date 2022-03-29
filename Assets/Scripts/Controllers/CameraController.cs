using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BulletHellElement 
{

    private CameraModel cameraModel;
    private CameraView cameraView;

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
        cameraModel = app.modelBase.cameraModel;
        cameraView = app.viewBase.cameraView;

        cameraBobHeight = cameraModel.cameraBobHeight;
        cameraBobSpeed = cameraModel.cameraBobSpeed;
        cameraBobTimer = cameraModel.cameraBobTimer;
        cameraDefaultPosition = app.viewBase.cameraView.transform.position;
        cameraDefaultRotation = app.viewBase.cameraView.transform.rotation.eulerAngles;
        cameraRotationSpeed = cameraModel.cameraRotationSpeed;
        maxCameraRotationAngleX = cameraModel.maxCameraRotationAngleX;
        maxCameraRotationAngleZ = cameraModel.maxCameraRotationAngleZ;
    }

    private void Update()
    {
        // bob the camera up and down
        cameraBobTimer += cameraBobSpeed * Time.deltaTime;
        cameraView.transform.position = new Vector3(cameraDefaultPosition.x, 
            cameraDefaultPosition.y + Mathf.Sin(cameraBobTimer) * cameraBobHeight, 
            cameraDefaultPosition.z);
    }
}
