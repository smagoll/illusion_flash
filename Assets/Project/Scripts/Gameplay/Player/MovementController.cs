using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;

    private Vector3 _currentVelocity; // Для сглаживания движения
    private Vector3 _smoothDirection; // Текущее сглаженное направление
    private float _verticalVelocity;  // Вертикальное движение (гравитация/прыжок)

    public void Move(Vector2 inputDirection)
    {
        ApplyHorizontalMovement(inputDirection);
        ApplyGravity();

        Vector3 move = (_smoothDirection * moveSpeed) + Vector3.up * _verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    private void ApplyHorizontalMovement(Vector2 input)
    {
        Vector3 targetDirection = new Vector3(input.x, 0, input.y).normalized;

        // Сглаживаем движение для мягкости
        _smoothDirection = Vector3.SmoothDamp(
            _smoothDirection, 
            targetDirection, 
            ref _currentVelocity, 
            smoothTime
        );
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            _verticalVelocity = -1f;

        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }
}