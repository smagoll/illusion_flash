using UnityEngine;

public class CharacterIdleState : CharacterState
{
    public CharacterIdleState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }
    
    public override void OnMoveInput(Vector2 input, MovementSpeedType speedType)
    {
        _stateMachine.SetState<CharacterLocomotionState>();
        _stateMachine.CurrentState.OnMoveInput(input, speedType);
    }
}