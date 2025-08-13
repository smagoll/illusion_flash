using Input;
using NodeCanvas.Framework;
using UnityEngine;

public class PlayerController : ICharacterController
{
    private readonly IInputService _input;
    private readonly ICameraService _cameraService;
    private Character character;

    public PlayerController(IInputService input, ICameraService cameraService)
    {
        _input = input;
        _cameraService = cameraService;
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
        
        LockOn();
    }

    private void Move()
    {
        if (_input.RunPressed)
        {
            character.MovementController.HandleMovement(_input.MoveAxis, MovementSpeedType.Run);   
        }
        else
        {
            if (_input.AltPressed)
            {
                character.MovementController.HandleMovement(_input.MoveAxis, MovementSpeedType.Walk);
            }
            else
            {
                character.MovementController.HandleMovement(_input.MoveAxis, MovementSpeedType.NormalRun);   
            }
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