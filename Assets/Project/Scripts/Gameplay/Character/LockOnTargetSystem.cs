using UnityEngine;

public class LockOnTargetSystem
{
    private MovementController _movementController;
    private LayerMask targetLayerMask;
    private float maxDistance;
    private float maxAngle;
    
    private ITargetable currentTarget;

    public ITargetable CurrentTarget => currentTarget;

    public LockOnTargetSystem(LayerMask targetLayerMask, MovementController movementController, float maxDistance = 15f, float maxAngle = 45f)
    {
        this.targetLayerMask = targetLayerMask;
        this.maxDistance = maxDistance;
        this.maxAngle = maxAngle;
        _movementController = movementController;
    }

    public void FindTarget(Vector3 origin, Vector3 forward)
    {
        Collider[] colliders = Physics.OverlapSphere(origin, maxDistance, targetLayerMask);

        ITargetable bestTarget = null;
        float bestScore = float.MinValue;

        foreach (var collider in colliders)
        {
            var target = collider.GetComponent<ITargetable>();
            if (target == null || !target.CanTarget)
                continue;

            Transform t = target.GetTransform();
            Vector3 direction = t.position - origin;
            float distance = direction.magnitude;

            direction.Normalize();
            float angle = Vector3.Angle(forward, direction);
            if (angle > maxAngle)
                continue;

            float score = (maxDistance - distance) - angle;

            if (score > bestScore)
            {
                bestScore = score;
                bestTarget = target;
            }
        }

        currentTarget = bestTarget;
    }

    public void Unlock()
    {
        currentTarget = null;
        _movementController.Unlock();
        _movementController.CameraService.Unlock();
    }

    public void Lock()
    {
        FindTarget(_movementController.transform.position, _movementController.CameraService.Forward);

        if (currentTarget != null)
        {
            _movementController.LockOn(currentTarget.GetTransform());
            _movementController.CameraService.LockOn(currentTarget.GetTransform());
            Debug.Log("Locked on target: " + currentTarget.GetTransform().name);
        }
        else
        {
            Debug.Log("Target not found!");
        }
    }
    
    public void TriggerLockOn()
    {
        if (CurrentTarget == null)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }
}