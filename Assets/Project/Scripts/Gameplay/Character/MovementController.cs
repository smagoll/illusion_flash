using UnityEngine;
using Zenject;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
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

    private ICameraService _cameraService;
    
    [Inject]
    private void Construct(ICameraService cameraService)
    {
        _cameraService = cameraService;
    }
    
    public void Move(Vector2 inputDirection)
    {
        ApplyHorizontalMovement(inputDirection);
        ApplyGravity();
        RotateTowardsMovement();

        Vector3 move = (_smoothDirection * moveSpeed) + Vector3.up * _verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    private void ApplyHorizontalMovement(Vector2 input)
    {
        // 1. Получаем forward и right камеры
        Vector3 camForward = _cameraService.Forward;
        Vector3 camRight = Vector3.Cross(Vector3.up, camForward);

        // 2. Убираем Y, чтобы не учитывался наклон камеры
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // 3. Переводим оси ввода в мировое направление
        Vector3 targetDirection = (camForward * input.y + camRight * input.x).normalized;

        // 4. Сглаживаем движение
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
        }
    }
}