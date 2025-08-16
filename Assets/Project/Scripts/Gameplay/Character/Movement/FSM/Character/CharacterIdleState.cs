using UnityEngine;

public class CharacterIdleState : CharacterState
{
    public CharacterIdleState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Update()
    {
        
    }
    
    public override void OnMoveInput(Vector2 input, MovementSpeedType speedType)
    {
        _stateMachine.TrySetState<CharacterLocomotionState>();
        _stateMachine.CurrentState.OnMoveInput(input, speedType);
    }

    public override void OnRotation()
    {
        _character.MovementController.Rotation();
    }
}