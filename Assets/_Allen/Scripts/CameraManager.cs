using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;   

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [Header("Attach Transforms")]
    [SerializeField] private Transform neutralPoint;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void SetCameraPosition(Transform attachPoint)
    {
        if (attachPoint != null)
        {
            cam.transform.position = attachPoint.position;
            cam.focalLength = Mathf.Lerp(cam.focalLength, 40, .3f);
        }
        else
        {
            cam.transform.position = neutralPoint.position;
            cam.focalLength = Mathf.Lerp(cam.focalLength, 50, 1f);
        }
    }

}
