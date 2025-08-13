using UnityEngine;

public class LockOnMovementState : MovementState
{
    private Vector3 smoothDirection;
    private Vector3 currentVelocity;
    
    public LockOnMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller)
    {
    }


    public override void SetSpeedType(MovementSpeedType speedType)
    {
        switch (speedType)
        {
            case MovementSpeedType.Walk:
            case MovementSpeedType.NormalRun:
                _stateMachine.ModeStateMachine.SetState(MovementModeType.Strafe);
                break;
            case MovementSpeedType.Run:
                _stateMachine.ModeStateMachine.SetState(MovementModeType.Free);
                break;
        }
    }
}