using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    // Camera
    public float sceneWidth = 12f;
    public CinemachineVirtualCamera vcamera;

    // Start is called before the first frame update
    void Start()
    {
        // Adjust camera to fit field        
        // Reference:
        // https://forum.unity.com/threads/how-to-programmatically-change-virtual-cams-orthographic-size.499491/
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        vcamera.m_Lens.OrthographicSize = desiredHalfHeight;
    }
}
