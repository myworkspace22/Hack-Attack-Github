using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10;

    private Camera ZoomCamera;

    public float MaxZoom = 10;

    public float MinZoom = 1;

    void Start()
    {
        ZoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoomCamera.orthographic)
        {
            ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

            if (ZoomCamera.orthographicSize > MaxZoom)
            {
                ZoomCamera.orthographicSize = MaxZoom;
            }
            if (ZoomCamera.orthographicSize < MinZoom)
            {
                ZoomCamera.orthographicSize = MinZoom;
            }
        }
        else
        {
            ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        }
    }
}
