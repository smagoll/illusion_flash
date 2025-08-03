using Input;
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
        
        character.Blackboard.GlobalBlackboard.SetValue(BBKeys.PlayerTransform, character.gameObject.transform);
    }

    public void Tick()
    {
        Move();

        if (_input.JumpPressed)
        {
            character.Movement.Jump();
        }

        UpdateBlackboard();

        if (_input.FirstItemPressed)
        {
            character.ToggleWeapon();
        }
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
            character.Movement.Walk(new Vector2(moveDir.x, moveDir.z));
        }
        else
        {
            character.Movement.Run(new Vector2(moveDir.x, moveDir.z));   
        }
    }

    private void UpdateBlackboard()
    {
        
    }
}