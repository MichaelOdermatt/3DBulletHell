using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : MonoBehaviour
{

    public float cameraBobHeight = 0.75f;
    public float cameraBobSpeed = 1.5f;
    public float cameraBobTimer = 0f;
    public Vector3 cameraDefaultPosition;
    public Vector3 cameraDefaultRotation;
    public float cameraRotationSpeed = 1.2f;
    public float maxCameraRotationAngleX = 10f;
    public float maxCameraRotationAngleZ = 10f;

}
