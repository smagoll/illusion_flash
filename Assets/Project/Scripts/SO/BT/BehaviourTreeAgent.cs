using System;
using UnityEngine;

public class BehaviourTreeAgent : MonoBehaviour
{
    private AIController _controller;
    
    public BehaviourTree BehaviourTree => _controller?.BehaviourTree;

    public void Init(AIController controller)
    {
        _controller = controller;
    }

    private void OnDrawGizmos()
    {
        _controller.DebugDraw();
    }
}