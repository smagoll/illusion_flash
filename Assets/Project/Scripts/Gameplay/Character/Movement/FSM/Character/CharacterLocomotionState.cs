using UnityEngine;

public class CharacterLocomotionState : CharacterState
{

    public CharacterLocomotionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        
    }

    public override void Enter()
    {
        _character.MovementController.ResumeMove();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _character.MovementController.StopMove();
    }
    
    public override void OnMoveInput(Vector2 input, MovementSpeedType speedType)
    {
        _character.MovementController.HandleMovement(input, speedType);
    }

    public override void OnStopMoveInput()
    {
        _stateMachine.SetState<CharacterIdleState>();
    }
}