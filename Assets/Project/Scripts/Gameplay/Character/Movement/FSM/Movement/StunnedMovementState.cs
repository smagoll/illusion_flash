using UnityEngine;

public class StunnedMovementState : MovementState
{
    public StunnedMovementState(MovementStateMachine stateMachine, MovementController controller) : base(stateMachine, controller)
    {
    }

    public override void Walk(Vector2 input)
    {
        
    }

    public override void NormalRun(Vector2 input)
    {
        
    }

    public override void Run(Vector2 input)
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
}