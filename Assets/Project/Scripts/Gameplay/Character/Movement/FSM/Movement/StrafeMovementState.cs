using UnityEngine;

public class StrafeMovementState : MovementState
{
    private Transform _target;
    private Vector3 smoothDirection;
    private Vector3 currentVelocity;
    
    public StrafeMovementState(MovementController controller, Transform target) : base(controller)
    {
        _target = target;
    }
    
    public override void HandleMovement(Vector2 input)
    {
        var inputDirection = _controller.GetInputFromCamera(input);
        
        _controller.AnimationController.UpdateDirection(input);
        Vector3 targetDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        smoothDirection = Vector3.SmoothDamp(smoothDirection, targetDirection, ref currentVelocity, _controller.MovementConfig.smoothTime);
        _controller.ApplyMovement(smoothDirection);
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
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        _controller.transform.rotation = Quaternion.Slerp(
            _controller.transform.rotation,
            targetRotation,
            _controller.MovementConfig.rotationSpeed * Time.deltaTime
        );
    }

    public override void Enter()
    {
        _controller.AnimationController.EnableDisableLockOn(true);
        _controller.SetSpeed(_controller.MovementConfig.walkSpeed);
    }

    public override void Exit()
    {
        _controller.AnimationController.EnableDisableLockOn(false);
        _controller.SetSpeed(_controller.MovementConfig.normalSpeed);
    }
}