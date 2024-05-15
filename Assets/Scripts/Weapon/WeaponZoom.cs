using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;

    [SerializeField] float zoomedOutFOV = 40f;
    [SerializeField] float zoomedInFOV = 25f;
    bool zoomedInToggle = false;

    // Mouse sensitivity
    FirstPersonController fpsController;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Start()
    {
        fpsController = GetComponentInParent<FirstPersonController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            ProcessZoom();
        }
    }

    private void ProcessZoom()
    {
        if(!zoomedInToggle)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.m_Lens.FieldOfView = zoomedInFOV;

        // Mouse sensitivity
        fpsController.RotationSpeed = zoomInSensitivity;
    }

    void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;

        // Mouse sensitivity
        fpsController.RotationSpeed = zoomOutSensitivity;
    }
}
