using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BulletHellElement, IController
{

    private CameraModel cameraModel;
    private CameraView cameraView;

    private float horizontal;
    private float vertical;
    private float cameraBobTimer;

    public Vector3 cameraDefaultPosition;
    public Vector3 cameraDefaultRotation;

    private void Awake()
    {
        cameraModel = app.modelContainer.cameraModel;
        cameraView = app.viewContainer.cameraView;

        cameraDefaultPosition = cameraView.transform.position;
        cameraDefaultRotation = cameraView.transform.rotation.eulerAngles;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // set camera XZ rotation when an input is given
        Quaternion cameraTarget = Quaternion.Euler(cameraDefaultRotation.x + cameraModel.maxCameraRotationAngleX * vertical * -1 , 
            cameraDefaultRotation.y, 
            cameraModel.maxCameraRotationAngleZ * horizontal * -1);

        cameraView.transform.rotation = Quaternion.Slerp(cameraView.transform.rotation, 
            cameraTarget, 
            cameraModel.cameraRotationSpeed * Time.deltaTime);

        // bob the camera up and down
        cameraBobTimer += cameraModel.cameraBobSpeed * Time.deltaTime;
        cameraView.transform.position = new Vector3(cameraDefaultPosition.x, 
            cameraDefaultPosition.y + Mathf.Sin(cameraBobTimer) * cameraModel.cameraBobHeight, 
            cameraDefaultPosition.z);
    }

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
    }
}
