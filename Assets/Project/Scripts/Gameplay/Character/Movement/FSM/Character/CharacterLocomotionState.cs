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
}