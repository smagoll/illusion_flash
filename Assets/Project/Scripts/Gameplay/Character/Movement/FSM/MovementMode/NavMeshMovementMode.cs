using UnityEngine;

public class NavMeshMovementMode : FreeMovementMode
{
    public NavMeshMovementMode(MovementController controller) : base(controller)
    {
    }

    public override void HandleMovement(Vector2 input)
    {
        Vector3 targetDirection = new Vector3(input.x, 0, input.y);
        smoothDirection = Vector3.SmoothDamp(smoothDirection, targetDirection, ref currentVelocity, _controller.MovementConfig.smoothTime);
        _controller.ApplyMovement(smoothDirection);
    }
}