using UnityEngine;

public class FreeMovementState : MovementState
{
    public FreeMovementState(MovementController controller) : base(controller) { }

    private Vector3 smoothDirection;
    private Vector3 currentVelocity;
    
    public override void HandleMovement(Vector2 input, float speed)
    {
        Vector3 targetDirection = new Vector3(input.x, 0, input.y);
        smoothDirection = Vector3.SmoothDamp(smoothDirection, targetDirection, ref currentVelocity, _controller.MovementConfig.smoothTime);
        _controller.ApplyMovement(smoothDirection * speed);
    }

    public override void HandleRotation()
    {
        Vector3 horizontalDir = new Vector3(smoothDirection.x, 0, smoothDirection.z);

        if (horizontalDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalDir, Vector3.up);
            _controller.transform.rotation = Quaternion.Slerp(
                _controller.transform.rotation,
                targetRotation,
                _controller.MovementConfig.rotationSpeed * Time.deltaTime
            );
        }
    }
}