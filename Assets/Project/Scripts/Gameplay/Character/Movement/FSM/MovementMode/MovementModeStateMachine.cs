using UnityEngine;

public class MovementModeStateMachine
{
    private MovementMode _prevState;
    private MovementMode _currentState;

    private FreeMovementMode _freeMovementMode;
    private StrafeMovementMode _strafeMovementMode;
    private NavMeshMovementMode _navMeshMovementMode;

    public MovementMode CurrentState => _currentState;

    public MovementModeStateMachine(MovementController controller)
    {
        _freeMovementMode = new FreeMovementMode(controller);
        _strafeMovementMode = new StrafeMovementMode(controller);
        _navMeshMovementMode = new NavMeshMovementMode(controller);

        SetState(_freeMovementMode);
    }

    public void SetState(MovementMode newState)
    {
        if (newState == _currentState) return;

        _prevState = _currentState;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void SetState(MovementModeType type)
    {
        switch (type)
        {
            case MovementModeType.Free:
                SetState(_freeMovementMode);
                break;
            case MovementModeType.Strafe:
                SetState(_strafeMovementMode);
                break;
            case MovementModeType.NavMesh:
                SetState(_navMeshMovementMode);
                break;
        }
    }

    public void Tick() => _currentState?.Tick();
    public void HandleMovement(Vector2 input) => _currentState?.HandleMovement(input);
    public void HandleRotation() => _currentState?.HandleRotation();

    public void RestorePreviousState()
    {
        if (_prevState != null)
            SetState(_prevState);
    }

    public void SetTarget(Transform target)
    {
        _strafeMovementMode.SetTarget(target);
    }
}

public enum MovementModeType
{
    Free,
    Strafe,
    NavMesh
}