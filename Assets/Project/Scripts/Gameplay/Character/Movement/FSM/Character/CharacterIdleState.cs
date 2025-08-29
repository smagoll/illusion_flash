using UnityEngine;

public class CharacterIdleState : CharacterState
{
    public CharacterIdleState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Update()
    {
        if (_character.MovementController.CurrentSpeed > 0.01f)
        {
            _stateMachine.TrySetState<CharacterLocomotionState>();
        }
    }
}