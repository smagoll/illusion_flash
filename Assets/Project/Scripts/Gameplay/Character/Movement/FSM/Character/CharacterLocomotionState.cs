using UnityEngine;

public class CharacterLocomotionState : CharacterState
{

    public CharacterLocomotionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        
    }

    public override void Update()
    {
        if (_character.MovementController.CurrentSpeed < 0.01f)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public override void Exit()
    {
        _character.AnimationController.UpdateDirection(Vector2.zero);
    }
}