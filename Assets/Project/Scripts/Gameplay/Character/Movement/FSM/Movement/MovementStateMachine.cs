using UnityEngine;

public class MovementStateMachine
{
    private MovementState _prevState;
    private MovementState _currentState;

    public MovementState CurrentState => _currentState;

    public void SetState(MovementState newState)
    {
        if (newState == _currentState) return;
        
        _prevState = _currentState;
        
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Tick() => _currentState?.Tick();
    public void HandleMovement(Vector2 input, float speed) => _currentState?.HandleMovement(input, speed);
    public void HandleRotation() => _currentState?.HandleRotation();
    
    public void RestorePreviousState()
    {
        if (_prevState != null)
            SetState(_prevState);
    }
}