using System;
using UnityEngine;
using Zenject;

public class MovementController : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private CharacterController characterController;

    [Header("Movement Settings")] [SerializeField]
    private float runSpeed = 7f;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump & Gravity")] [SerializeField]
    private float gravity = -9.81f;

    private Vector3 _currentVelocity; // Для сглаживания движения
    private Vector3 _smoothDirection; // Текущее сглаженное направление
    private float _verticalVelocity; // Вертикальное движение (гравитация/прыжок)
    private Vector3 _moveDirection; // Итоговое накопленное направление
    private Vector3 _attackImpulse;

    private bool isMove = true;

    public float VerticalSpeed => characterController.velocity.y;
    public float HorizontalSpeed => new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
    public float TotalSpeed => characterController.velocity.magnitude;
    
    public Vector3 Forward => characterController.transform.forward;
    public bool IsGrounded => characterController.isGrounded;
    
    private AnimationController _animationController;

    public void Init(AnimationController animationController)
    {
        _animationController = animationController;
    }

    private void Update()
    {
        ApplyGravity();

        Vector3 move = _moveDirection + Vector3.up * _verticalVelocity + _attackImpulse;
        characterController.Move(move * Time.deltaTime);

        _animationController.UpdateSpeed(HorizontalSpeed);
        _animationController.UpdateIsFalling(!characterController.isGrounded && VerticalSpeed < -1);

        RotateTowardsMovement();
        
        if (_attackImpulse.sqrMagnitude > 0.01f)
        {
            _moveDirection += _attackImpulse;
            _attackImpulse = Vector3.Lerp(_attackImpulse, Vector3.zero, Time.deltaTime * 5f); // затухание
        }
        
        _moveDirection = Vector3.zero; 
    }

    public void Walk(Vector2 inputDirection)
    {
        Move(inputDirection, walkSpeed);
    }

    public void Run(Vector2 inputDirection)
    {
        Move(inputDirection, runSpeed);
    }

    public void Move(Vector2 inputDirection, float speed)
    {
        if (!isMove) return;
        
        ApplyHorizontalMovement(inputDirection);
        _moveDirection += _smoothDirection * speed;
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
                _verticalVelocity = -1f; // оставляем небольшой "прижим"
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
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    public void ApplyVerticalVelocity(float velocity)
    {
        _verticalVelocity = velocity;
    }

    public void StopMove()
    {
        isMove = false;
    }

    public void ResumeMove()
    {
        isMove = true;
    }
    
    public void ApplyImpulse(Vector3 direction, float strength)
    {
        _attackImpulse = direction.normalized * strength;
    }
}
