﻿using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //sets up player camera
    public static CameraManager Instance;

    public CinemachineVirtualCamera VirtualCamera;

    private void Awake()
    {
        Instance = this;
    }

    public void Attach(TankPawn tankPawn)
    {
        VirtualCamera.Follow = tankPawn.cameraFollow;
        VirtualCamera.LookAt = tankPawn.cameraLookAt;
    }

}
