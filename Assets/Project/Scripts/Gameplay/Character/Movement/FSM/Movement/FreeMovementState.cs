using UnityEngine;

public class FreeMovementState : MovementState
{
    public FreeMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller) { }

    protected Vector3 smoothDirection;
    protected Vector3 currentVelocity;
    
    public override void Walk(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
    }

    public override void NormalRun(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
    }

    public override void Run(Vector2 input)
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
    }
}