using UnityEngine;

public interface ITargetable
{
    Transform GetTransform();
    bool CanTarget { get; }
    Transform LockOnPoint { get; }
}