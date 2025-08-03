using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private AnimationController animationController;

    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;

    private Vector3 _currentVelocity; // Для сглаживания движения
    private Vector3 _smoothDirection; // Текущее сглаженное направление
    private float _verticalVelocity;  // Вертикальное движение (гравитация/прыжок)

    public float VerticalSpeed => characterController.velocity.y; // только по Y
    public float HorizontalSpeed => new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude; // только по XZ
    public float TotalSpeed => characterController.velocity.magnitude; // вся скорость

    private void Update()
    {
        animationController.UpdateSpeed(HorizontalSpeed);
        animationController.UpdateIsFalling(!characterController.isGrounded && VerticalSpeed < -1);
    }

    public void Move(Vector2 inputDirection, float speed)
    {
        ApplyHorizontalMovement(inputDirection);
        ApplyGravity();
        RotateTowardsMovement();
        
        Vector3 move = (_smoothDirection * speed) + Vector3.up * _verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    public void Walk(Vector2 inputDirection)
    {
        Move(inputDirection, walkSpeed);
    }
    
    public void Run(Vector2 inputDirection)
    {
        Move(inputDirection, runSpeed);
    }

    private void ApplyHorizontalMovement(Vector2 input)
    {
        Vector3 targetDirection = new Vector3(input.x, 0, input.y);

        _smoothDirection = Vector3.SmoothDamp(_smoothDirection, targetDirection, ref _currentVelocity, smoothTime);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            if (_verticalVelocity < 0)
                _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }
    
    private void RotateTowardsMovement()
    {
        Vector3 horizontalDir = new Vector3(_smoothDirection.x, 0, _smoothDirection.z);

        if (horizontalDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalDir, Vector3.up);

            // Плавно поворачиваем персонажа
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            _verticalVelocity = jumpForce;
            animationController.Jump();
        }
    }
}