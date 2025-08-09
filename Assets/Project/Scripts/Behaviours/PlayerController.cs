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
        Vector3 camForward = _cameraService.Forward;
        Vector3 camRight = Vector3.Cross(Vector3.up, camForward);

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        Vector3 moveDir = (camForward * _input.MoveAxis.y + camRight * _input.MoveAxis.x).normalized;

        if (_input.AltPressed)
        {
            character.MovementController.Walk(new Vector2(moveDir.x, moveDir.z));
        }
        else
        {
            character.MovementController.Run(new Vector2(moveDir.x, moveDir.z));   
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