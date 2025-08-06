using UnityEngine;

public class LockedOnMovementState : MovementState
{
    private Transform _target;

    public LockedOnMovementState(MovementController controller, Transform target) : base(controller)
    {
        _target = target;
    }

    public override void HandleMovement(Vector2 input, float speed)
    {
        
    }

    public override void HandleRotation()
    {
        
    }

    public override void Exit()
    {
        _target = null;
    }
}