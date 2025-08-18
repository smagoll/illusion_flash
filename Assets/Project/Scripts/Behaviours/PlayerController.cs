using Input;
using NodeCanvas.Framework;
using UnityEngine;

public class PlayerController : ICharacterController
{
    private readonly IInputService _input;
    private Character character;

    public PlayerController(IInputService input)
    {
        _input = input;
    }

    public void Init(Character character)
    {
        this.character = character;

        character.GlobalBlackboard.AddVariable(BBKeys.PlayerCharacter,  character);
        character.Model.IsPlayer = true;
    }

    public void Tick()
    {
        Move();

        if (_input.JumpPressed)
        {
            //character.AbilityController.TryExecute(AbilityKeys.Jump);
        }

        if (_input.FirstItemPressed)
        {
            character.WeaponController.ToggleWeapon();
        }

        if (_input.AttackPressed)
        {
            character.AbilityController.TryExecute(AbilityKeys.Attack);
        }

        if (_input.DodgePressed)
        {
            character.AbilityController.TryExecute(AbilityKeys.Dodge);
        }
        
        character.Block(_input.BlockPressed);
        
        LockOn();
    }

    private void Move()
    {
        Vector2 axis = _input.MoveAxis;
    
        if (axis.sqrMagnitude > 0.01f)
        {
            MovementSpeedType speedType;
            if (_input.RunPressed)
                speedType = MovementSpeedType.Run;
            else if (_input.AltPressed)
                speedType = MovementSpeedType.Walk;
            else
                speedType = MovementSpeedType.NormalRun;

            character.StateMachine.CurrentState.OnMoveInput(axis, speedType);
        }
        else
        {
            character.StateMachine.CurrentState.OnStopMoveInput();
        }
    }

    private void LockOn()
    {
        if (_input.LockOnPressed)
        {
            character.LockOnTargetSystem.TriggerLockOn();
        }
    }
}