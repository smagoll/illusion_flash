using UnityEngine;

public interface ICameraService
{
    Quaternion Rotation { get; }
    Vector3 Forward { get; }
    void SetTrackingTarget(Transform target);
    void LockOn(Transform target);
    void Unlock();
}