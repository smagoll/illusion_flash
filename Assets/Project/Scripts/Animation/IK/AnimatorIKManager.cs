using System.Collections.Generic;
using UnityEngine;

public class AnimatorIKManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("IK Foot Config")]
    [SerializeField] private IKFootConfig footConfig = default;

    private readonly List<IAnimatorIKController> handlers = new();

    private void Awake()
    {
        var footController = new IKFootController(animator, footConfig);
        RegisterHandler(footController);
    }

    public void RegisterHandler(IAnimatorIKController handler)
    {
        if (!handlers.Contains(handler))
            handlers.Add(handler);
    }

    public void UnregisterHandler(IAnimatorIKController handler)
    {
        if (handlers.Contains(handler))
            handlers.Remove(handler);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        foreach (var handler in handlers)
            handler.OnAnimatorIK(layerIndex);
    }

    private void OnDrawGizmos()
    {
        if (handlers == null) return;

        foreach (var handler in handlers)
            handler.OnDrawGizmos();
    }
}