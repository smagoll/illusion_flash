using UnityEngine;

public class StunnedMovementState : MovementState
{
    public StunnedMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        Debug.Log("enter stun");
    }

    public override void Exit()
    {   
        Debug.Log("exit stun");
    }

    public override void SetSpeedType(MovementSpeedType speedType)
    {
        
    }
}