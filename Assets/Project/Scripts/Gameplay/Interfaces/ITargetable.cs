using UnityEngine;

public interface ITargetable
{
    Transform GetTransform();
    bool CanTarget { get; }
}