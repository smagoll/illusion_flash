using UnityEngine;

public class LockOnTargetSystem
{
    private LayerMask targetLayerMask;
    private float maxDistance;
    private float maxAngle;

    public ITargetable CurrentTarget { get; private set; }

    public LockOnTargetSystem(LayerMask targetLayerMask, float maxDistance = 15f, float maxAngle = 45f)
    {
        this.targetLayerMask = targetLayerMask;
        this.maxDistance = maxDistance;
        this.maxAngle = maxAngle;
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

        CurrentTarget = bestTarget;
    }

    public void Unlock()
    {
        CurrentTarget = null;
    }
}