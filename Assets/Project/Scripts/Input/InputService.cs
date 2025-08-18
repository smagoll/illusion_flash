using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
        bool JumpPressed { get; }
        bool AltPressed { get; }
        bool RunPressed { get; }
        bool DodgePressed { get; }
        bool FirstItemPressed { get; }
        bool AttackPressed { get; }
        bool LockOnPressed { get; }
        bool BlockPressed { get; }
    }

    public class InputService : MonoBehaviour, IInputService
    {
        private PlayerInput _inputActions;
        private Vector2 _moveAxis;
        private bool _jumpPressed;
        private bool _altPressed;
        private bool _runPressed;
        private bool _firstItemPressed;
        private bool _attackPressed;
        private bool _lockOnPressed;
        private bool _dodgePressed;
        private bool _blockPressed;

        public Vector2 MoveAxis => _moveAxis;
        public bool JumpPressed => _jumpPressed;
        public bool AltPressed => _altPressed;
        public bool RunPressed => _runPressed;
        public bool FirstItemPressed => _firstItemPressed;
        public bool AttackPressed => _attackPressed;
        public bool LockOnPressed => _lockOnPressed;
        public bool DodgePressed => _dodgePressed;
        public bool BlockPressed => _blockPressed;

        private void Awake()
        {
            _inputActions = new PlayerInput();

            // move
            _inputActions.Player.Move.performed += ctx => _moveAxis = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += _ => _moveAxis = Vector2.zero;

            // jump
            _inputActions.Player.Jump.performed += _ => _jumpPressed = true;
            
            // dodge
            _inputActions.Player.Dodge.performed += _ => _dodgePressed = true;
            
            // walk
            _inputActions.Player.Walk.performed += _ => _altPressed = true;
            _inputActions.Player.Walk.canceled += _ => _altPressed = false;
            
            // run
            _inputActions.Player.Run.performed += _ => _runPressed = true;
            _inputActions.Player.Run.canceled += _ => _runPressed = false;
            
            // actions
            _inputActions.Player.Attack.performed += _ => _attackPressed = true;
            _inputActions.Player.LockOn.performed += _ => _lockOnPressed = true;
            
            // block
            _inputActions.Player.Block.performed += _ => _blockPressed = true;
            _inputActions.Player.Block.canceled += _ => _blockPressed = false;
            
            // inventory
            _inputActions.Player.FirstItem.performed += _ => _firstItemPressed = true;
        }

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();

        private void LateUpdate()
        {
            // Сброс после обработки
            _jumpPressed = false;
            _firstItemPressed = false;
            _attackPressed = false;
            _lockOnPressed = false;
            _dodgePressed = false;
        }
    }
}
