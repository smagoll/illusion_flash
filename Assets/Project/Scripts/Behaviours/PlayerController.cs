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
            character.AbilityController.TryExecute(AbilityKeys.Jump);
        }

        if (_input.FirstItemPressed)
        {
            character.WeaponController.ToggleWeapon();
        }

        if (_input.AttackPressed)
        {
            character.AbilityController.TryExecute(AbilityKeys.Attack);
        }
        
        LockOn();
    }

    private void Move()
    {
        if (_input.AltPressed)
        {
            character.MovementController.Walk(_input.MoveAxis);
        }
        else
        {
            character.MovementController.Run(_input.MoveAxis);   
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