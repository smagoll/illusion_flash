using UnityEngine;

public class StunnedMovementState : MovementState
{
    public StunnedMovementState(MovementController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Debug.Log("to stun");
    }

    public override void Exit()
    {   
        Debug.Log("from stun");
    }
}