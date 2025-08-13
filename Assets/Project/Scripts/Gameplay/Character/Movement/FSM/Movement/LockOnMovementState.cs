using UnityEngine;

public class LockOnMovementState : MovementState
{
    private Vector3 smoothDirection;
    private Vector3 currentVelocity;
    
    public LockOnMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller)
    {
    }
    
    public override void Walk(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Strafe);
    }

    public override void NormalRun(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Strafe);
    }

    public override void Run(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
    }
}