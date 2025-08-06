using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover
{
    private MovementController _movementController;
    private NavMeshAgent _navMeshAgent;

    private bool isActive;
    private float stoppingDistance;
    
    public bool IsActive => isActive;

    public NavMeshMover(MovementController movementController, NavMeshAgent navMeshAgent)
    {
        _movementController = movementController;
        _navMeshAgent = navMeshAgent;

        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        stoppingDistance = _navMeshAgent.stoppingDistance;
    }

    public void MoveTo(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
        isActive = true;
        _navMeshAgent.isStopped = false;
    }
    public void Stop()
    {
        isActive = false;
        _navMeshAgent.isStopped = true;
    }

    public void Tick()
    {
        if (!isActive) return;
        
        // Проверяем, достигли ли цели
        if (_navMeshAgent.remainingDistance <= stoppingDistance && !_navMeshAgent.pathPending)
        {
            Stop();
            return;
        }
        
        // Получаем желаемое направление от NavMeshAgent
        Vector3 desiredVelocity = _navMeshAgent.desiredVelocity;
        desiredVelocity.y = 0;
        
        if (desiredVelocity.sqrMagnitude > 0.01f)
        {
            Vector2 inputDirection = new Vector2(desiredVelocity.x, desiredVelocity.z).normalized;
            _movementController.Move(inputDirection, _movementController.MovementConfig.walkSpeed);
        }
        
        _navMeshAgent.nextPosition = _movementController.transform.position;

        DebugDrawPath();
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
