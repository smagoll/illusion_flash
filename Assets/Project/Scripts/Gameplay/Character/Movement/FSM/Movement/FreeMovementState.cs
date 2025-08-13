using UnityEngine;

public class FreeMovementState : MovementState
{
    public FreeMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller) { }

    public override void SetSpeedType(MovementSpeedType speedType)
    {
        switch (speedType)
        {
            case MovementSpeedType.Walk:
            case MovementSpeedType.NormalRun:
            case MovementSpeedType.Run:
                _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
                break;
        }
    }
}