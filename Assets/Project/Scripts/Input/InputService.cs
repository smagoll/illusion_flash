using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
        bool JumpPressed { get; }
    }

    public class InputService : MonoBehaviour, IInputService
    {
        private PlayerInput _inputActions;
        private Vector2 _moveAxis;
        private bool _jumpPressed;

        public Vector2 MoveAxis => _moveAxis;
        public bool JumpPressed => _jumpPressed;

        private void Awake()
        {
            _inputActions = new PlayerInput();

            // Привязка событий
            _inputActions.Player.Move.performed += ctx => _moveAxis = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += _ => _moveAxis = Vector2.zero;

            _inputActions.Player.Jump.performed += _ => _jumpPressed = true;
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
