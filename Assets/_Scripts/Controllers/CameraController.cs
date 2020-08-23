using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        _camera.orthographicSize = 4.29f / aspectRatio;
    }
}
