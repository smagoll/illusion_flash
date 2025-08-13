using UnityEngine;

public class FreeMovementMode : MovementMode
{
    public FreeMovementMode(MovementController controller) : base(controller) { }

    protected Vector3 smoothDirection;
    protected Vector3 currentVelocity;
    
    public override void HandleMovement(Vector2 input)
    {
        var inputDirection = _controller.GetInputFromCamera(input);
        
        Vector3 targetDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        smoothDirection = Vector3.SmoothDamp(smoothDirection, targetDirection, ref currentVelocity, _controller.MovementConfig.smoothTime);
        _controller.ApplyMovement(smoothDirection);
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