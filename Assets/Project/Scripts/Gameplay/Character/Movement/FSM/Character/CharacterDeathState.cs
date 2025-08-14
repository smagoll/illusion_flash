using UnityEngine;

public class CharacterDeathState : CharacterState
{
    public CharacterDeathState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Enter()
    {
        _character.AnimationController.Death();
        _character.MovementController.StopMove();
        _character.MovementController.Collider.enabled = false;
        
        Debug.Log("CharacterDeathState Enter");
    }

    public override void Update()
    {
        
    }
}