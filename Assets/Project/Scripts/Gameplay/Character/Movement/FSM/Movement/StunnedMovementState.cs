using UnityEngine;

public class StunnedMovementState : MovementState
{
    public StunnedMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        _stateMachine.ModeStateMachine.SetState(MovementModeType.Stay);
    }

    public override void Exit()
    {   
        _stateMachine.ModeStateMachine.RestorePreviousState();
    }
}