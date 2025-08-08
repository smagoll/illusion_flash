using System;
using Input;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraService : MonoBehaviour, ICameraService
{
    [SerializeField] private CinemachineCamera _freeLook;
    [SerializeField] private CinemachineCamera lockOnCam;

    private CinemachineCamera currentCam;
    
    public Quaternion Rotation => currentCam.State.RawOrientation;
    public Vector3 Forward => currentCam.State.RawOrientation * Vector3.forward;

    public void SetTrackingTarget(Transform target)
    {
        currentCam = _freeLook;
        
        _freeLook.Target.TrackingTarget =  target;
        player = _freeLook.Follow;
        lockOnCam.gameObject.SetActive(false);
    }
    
    private Transform player;
    private Transform currentTarget;

    public void LockOn(Transform target)
    {
        currentCam = lockOnCam;
        
        currentTarget = target;
        lockOnCam.Follow = player;
        lockOnCam.LookAt = target;

        _freeLook.gameObject.SetActive(false);
        lockOnCam.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        currentCam = _freeLook;
        
        currentTarget = null;

        lockOnCam.gameObject.SetActive(false);
        _freeLook.gameObject.SetActive(true);
    }
}