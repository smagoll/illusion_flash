using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
        bool JumpPressed { get; }
        bool AltPressed { get; }
    }

    public class InputService : MonoBehaviour, IInputService
    {
        private PlayerInput _inputActions;
        private Vector2 _moveAxis;
        private bool _jumpPressed;
        private bool _altPressed;

        public Vector2 MoveAxis => _moveAxis;
        public bool JumpPressed => _jumpPressed;
        public bool AltPressed => _altPressed;

        private void Awake()
        {
            _inputActions = new PlayerInput();

            // move
            _inputActions.Player.Move.performed += ctx => _moveAxis = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += _ => _moveAxis = Vector2.zero;

            // jump
            _inputActions.Player.Jump.performed += _ => _jumpPressed = true;
            
            // walk
            _inputActions.Player.Walk.performed += _ => _altPressed = true;
            _inputActions.Player.Walk.canceled += _ => _altPressed = false;
        }

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();

        private void LateUpdate()
        {
            // Сброс после обработки
            _jumpPressed = false;
        }
    }
}
