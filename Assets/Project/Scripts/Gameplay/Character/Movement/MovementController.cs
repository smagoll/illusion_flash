using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MovementController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Movement Settings")] 
    [SerializeField] private MovementConfig movementConfig;

    [Header("Jump & Gravity")] [SerializeField]
    private float gravity = -9.81f;

    private Vector3 _currentVelocity; // Для сглаживания движения
    private float _verticalVelocity; // Вертикальное движение (гравитация/прыжок)
    private Vector3 _moveDirection; // Итоговое накопленное направление
    private Vector3 _impulse;

    private MovementStateMachine _movementStateMachine;
    
    private MovementState freeMovementState;
    private MovementState lockOnMovementState;
    private MovementState navMeshMovementState;
    private MovementState stunnedMovementState;

    [Inject] public ICameraService CameraService { get; }
    
    public MovementConfig MovementConfig => movementConfig;
    
    public float VerticalSpeed => characterController.velocity.y;
    public float HorizontalSpeed => new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
    public float TotalSpeed => characterController.velocity.magnitude;
    
    public Vector3 Forward => characterController.transform.forward;
    public bool IsGrounded => characterController.isGrounded;
    
    private AnimationController _animationController;

    public void Init(AnimationController animationController)
    {
        _animationController = animationController;
        
        _movementStateMachine = new MovementStateMachine();

        freeMovementState = new FreeMovementState(this);
        navMeshMovementState = new NavMeshMovementState(this, navMeshAgent);
        stunnedMovementState = new StunnedMovementState(this);
        
        _movementStateMachine.SetState(new FreeMovementState(this));
    }

    private void Update()
    {
        _movementStateMachine.Tick();
        
        ApplyGravity();

        Vector3 move = _moveDirection + Vector3.up * _verticalVelocity + _impulse;
        characterController.Move(move * Time.deltaTime);

        _animationController.UpdateSpeed(HorizontalSpeed);
        _animationController.UpdateIsFalling(!characterController.isGrounded && VerticalSpeed < -1);

        _movementStateMachine.HandleRotation();
        
        if (_impulse.sqrMagnitude > 0.01f)
        {
            _moveDirection += _impulse;
            _impulse = Vector3.Lerp(_impulse, Vector3.zero, Time.deltaTime * 5f);
        }
        
        _moveDirection = Vector3.zero; 
    }

    public void Walk(Vector2 inputDirection)
    {
        Move(inputDirection, movementConfig.walkSpeed);
    }

    public void Run(Vector2 inputDirection)
    {
        Move(inputDirection,  movementConfig.runSpeed);
    }

    public void Move(Vector2 inputDirection, float speed)
    {
        _movementStateMachine.HandleMovement(inputDirection, speed);
    }
    
    public void MoveTo(Vector3 destination, float speed)
    {
        _movementStateMachine.SetState(navMeshMovementState);
        
        if (_movementStateMachine.CurrentState is IMoveToTarget moveToState)
        {
            moveToState.MoveTo(destination);
        }
    }

    public void ApplyMovement(Vector3 velocity)
    {
        _moveDirection += velocity;
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

    public void ApplyVerticalVelocity(float velocity)
    {
        _verticalVelocity = velocity;
    }

    public void StopMove()
    {
        _movementStateMachine.SetState(stunnedMovementState);
    }

    public void ResumeMove()
    {
        Debug.Log("Resume move");
        
        _movementStateMachine.RestorePreviousState();
    }
    
    public void LockOn(Transform target)
    {
        lockOnMovementState = new LockedOnMovementState(this, target);
        _movementStateMachine.SetState(lockOnMovementState);
    }

    public void Unlock()
    {
        _movementStateMachine.SetState(freeMovementState);
    }

    
    public void ApplyImpulse(Vector3 direction, float strength)
    {
        _impulse = direction.normalized * strength;
    }
}
