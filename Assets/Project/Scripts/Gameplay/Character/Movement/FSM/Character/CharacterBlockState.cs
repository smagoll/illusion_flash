using UnityEngine;

public class CharacterBlockState : CharacterState
{
    public CharacterBlockState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        _character.AnimationController.Block(true);
    }

    public override void Update()
    {
        if (!_character.CombatSystem.BlockSystem.IsBlocked)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public override void OnMoveInput(Vector2 input, MovementSpeedType speedType)
    {
        _character.MovementController.HandleMovement(input, speedType);
    }

    public override void OnStopMoveInput()
    {
        _character.MovementController.HandleMovement(Vector2.zero, MovementSpeedType.Walk);
    }

    public override void OnRotation()
    {
        _character.MovementController.Rotation();
    }

    public override void Exit()
    {
        _character.CombatSystem.ParrySystem.TryParry();
        _character.AnimationController.Block(false);
    }
}