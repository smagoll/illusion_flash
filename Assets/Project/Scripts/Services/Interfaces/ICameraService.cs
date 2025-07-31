using UnityEngine;

public interface ICameraService
{
    Quaternion Rotation { get; }
    Vector3 Forward { get; }
    void SetTarget(Transform target);
}