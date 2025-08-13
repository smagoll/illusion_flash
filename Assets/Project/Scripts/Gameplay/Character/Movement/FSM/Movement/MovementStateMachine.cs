using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementStateMachine
{
    private MovementState _prevState;
    private MovementState _currentState;

    private MovementModeStateMachine _movementModeStateMachine;
    private Dictionary<Type, MovementState> _states;

    public MovementState CurrentState => _currentState;
    public MovementModeStateMachine ModeStateMachine => _movementModeStateMachine;

    public MovementStateMachine(MovementController controller, NavMeshAgent agent)
    {
        _movementModeStateMachine = new MovementModeStateMachine(controller);

        _states = new Dictionary<Type, MovementState>
        {
            { typeof(FreeMovementState), new FreeMovementState(this, controller) },
            { typeof(LockOnMovementState), new LockOnMovementState(this, controller) },
            { typeof(NavMeshMovementState), new NavMeshMovementState(this, controller, agent) },
            { typeof(StunnedMovementState), new StunnedMovementState(this, controller) }
        };

        SetState<FreeMovementState>();
    }

    public void SetState<T>() where T : MovementState
    {
        var newState = _states[typeof(T)];
        if (newState == _currentState) return;

        _prevState = _currentState;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Tick()
    {
        _currentState?.Tick();
        _movementModeStateMachine.Tick();
    }

    public void HandleMovement(Vector2 input, MovementSpeedType speedType)
    {
        _currentState.SetSpeedType(speedType);
        _movementModeStateMachine.HandleMovement(input);
    }
    public void HandleRotation() => _movementModeStateMachine?.HandleRotation();

    public void RestorePreviousState()
    {
        if (_prevState != null)
            SetState(_prevState.GetType());
    }

    public void SetState(Type stateType)
    {
        if (!_states.TryGetValue(stateType, out var newState))
            throw new Exception($"State {stateType} not registered in MovementStateMachine.");

        if (newState == _currentState) return;

        _prevState = _currentState;
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
