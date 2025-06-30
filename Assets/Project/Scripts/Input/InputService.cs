using UnityEngine;

namespace Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
        bool JumpPressed { get; }
    }
    
    public class InputService : MonoBehaviour, IInputService
    {
        private Vector2 _moveAxis;
        private bool _jumpPressed;

        public Vector2 MoveAxis => _moveAxis;
        public bool JumpPressed => _jumpPressed;

        private void Update()
        {
            _moveAxis = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
            _jumpPressed = UnityEngine.Input.GetButtonDown("Jump");
        }
    }
}