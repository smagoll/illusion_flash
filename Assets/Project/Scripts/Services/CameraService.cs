using Input;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraService : MonoBehaviour, ICameraService
{
    [SerializeField] private CinemachineCamera _freeLook;

    public Quaternion Rotation => _freeLook.State.RawOrientation;
    public Vector3 Forward => _freeLook.State.RawOrientation * Vector3.forward;

    public void SetTarget(Transform target)
    {
        _freeLook.Target.TrackingTarget =  target;
    }
}