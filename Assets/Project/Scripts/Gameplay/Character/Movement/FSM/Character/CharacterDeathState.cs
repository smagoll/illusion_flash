using UnityEngine;

public class CharacterDeathState : CharacterState
{
    public CharacterDeathState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Enter()
    {
        _character.AnimationController.Death();
        _character.MovementController.StopMove();
    }

    public override void Update()
    {
        
    }
}