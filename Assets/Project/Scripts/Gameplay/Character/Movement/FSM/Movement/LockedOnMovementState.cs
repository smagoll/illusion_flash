using UnityEngine;

public class LockedOnMovementState : MovementState
{
    private Transform _target;
    private Vector3 smoothDirection;
    private Vector3 currentVelocity;

    public LockedOnMovementState(MovementController controller, Transform target) : base(controller)
    {
        _target = target;
    }

    public override void HandleMovement(Vector2 input, float speed)
    {
        Vector3 targetDirection = new Vector3(input.x, 0, input.y);
        smoothDirection = Vector3.SmoothDamp(smoothDirection, targetDirection, ref currentVelocity, _controller.MovementConfig.smoothTime);
        _controller.ApplyMovement(smoothDirection * speed);
    }

    public override void HandleRotation()
    {
        if (_target == null)
        {
            Debug.LogWarning("LockedOnMovementState: Target is null!");
            return;
        }

        Vector3 directionToTarget = _target.position - _controller.transform.position;
        directionToTarget.y = 0;

        if (directionToTarget.sqrMagnitude < 0.001f)
        {
            Debug.Log("LockedOnMovementState: Target too close or at same position.");
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        _controller.transform.rotation = Quaternion.Slerp(
            _controller.transform.rotation,
            targetRotation,
            _controller.MovementConfig.rotationSpeed * Time.deltaTime
        );

        Debug.Log("LockedOnMovementState: Rotating towards target.");
    }
}