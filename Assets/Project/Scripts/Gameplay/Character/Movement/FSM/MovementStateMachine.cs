using UnityEngine;

public class MovementStateMachine
{
    private MovementState _prevState;
    private MovementState _currentState;

    public MovementState CurrentState => _currentState;
    public MovementState PrevState => _prevState;

    public void SetState(MovementState newState)
    {
        if (newState == _currentState) return;
        
        _currentState?.Exit();
        _prevState = _currentState;
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Tick() => _currentState?.Tick();
    public void HandleMovement(Vector2 input, float speed) => _currentState?.HandleMovement(input, speed);
    public void HandleRotation() => _currentState?.HandleRotation();
}