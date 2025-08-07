using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementState : FreeMovementState, IMoveToTarget
{
    private NavMeshAgent _navMeshAgent;
    
    private float stoppingDistance;
    
    public NavMeshMovementState(MovementController controller, NavMeshAgent navMeshAgent) : base(controller)
    {
        _navMeshAgent = navMeshAgent;

        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        stoppingDistance = _navMeshAgent.stoppingDistance;
    }

    public void MoveTo(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
        _navMeshAgent.isStopped = false;
    }
    
    public override void Tick()
    {
        if (_navMeshAgent.remainingDistance <= stoppingDistance && !_navMeshAgent.pathPending)
        {
            Stop();
            return;
        }
        
        Vector3 desiredVelocity = _navMeshAgent.desiredVelocity;
        desiredVelocity.y = 0;
        
        if (desiredVelocity.sqrMagnitude > 0.01f)
        {
            Vector2 inputDirection = new Vector2(desiredVelocity.x, desiredVelocity.z).normalized;
            controller.Move(inputDirection, controller.MovementConfig.walkSpeed);
        }
        
        _navMeshAgent.nextPosition = controller.transform.position;

        DebugDrawPath();
    }

    public override void Exit()
    {
        base.Exit();
        
        Stop();
    }

    private void Stop()
    {
        _navMeshAgent.isStopped = true;
    }
    
#if UNITY_EDITOR
    private void DebugDrawPath()
    {
        var path = _navMeshAgent.path;
        if (path.corners.Length < 2) return;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green);
        }
    }
#endif
}