using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;   

    private Camera cam;
    [Header("Attach Transforms")]
    [SerializeField] private Transform neutralPoint;
    [SerializeField] private Vector3 attachLookAtOffset;

    [Header("Camera Lerps")]
    [SerializeField] [Range(0, 5)] private float positionSmoothing;
    [SerializeField] [Range(0, 5)] private float fovSmoothing;

    private Vector3 targetPoint;
    private float targetFOV;

    Vector3 positionVelocity = Vector3.zero;
    float fovVelocity = 0f;

    private void Update()
    {
        if (cam.transform.position != targetPoint)
        {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPoint, ref positionVelocity, positionSmoothing);
        }

        if (cam.focalLength != targetFOV)
        {
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref fovVelocity, fovSmoothing);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        targetPoint = neutralPoint.position;

        GameObject cameraObject = new GameObject();
        cameraObject.AddComponent<Camera>();

        cam = Instantiate(cameraObject, neutralPoint).GetComponent<Camera>();
    }

    public void SetCameraPosition(Transform attachPoint)
    {
        if (attachPoint != null)
        {
            targetPoint = attachPoint.position + attachLookAtOffset;
            targetFOV = 50f;
        }
        else
        {
            targetPoint = neutralPoint.position;
            targetFOV = 80f;
        }
    }

}
