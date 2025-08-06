using UnityEngine;

public class MovementStateMachine
{
    private MovementState _currentState;

    public void SetState(MovementState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Tick() => _currentState?.Tick();
    public void HandleMovement(Vector2 input, float speed) => _currentState?.HandleMovement(input, speed);
    public void HandleRotation() => _currentState?.HandleRotation();
}