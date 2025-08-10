using System;
using System.Collections.Generic;

public class CharacterStateMachine
{
    private readonly Dictionary<Type, CharacterState> _states = new();
    public CharacterState CurrentState { get; private set; }
    
    public Character Character { get; private set; }

public CharacterStateMachine(Character character)
    {
        Character = character;

        AddState(new CharacterIdleState(this));
        AddState(new CharacterDeathState(this));
        AddState(new CharacterLocomotionState(this));
        AddState(new CharacterStunState(this));
        AddState(new CharacterAttackState(this));

        SetState<CharacterIdleState>();
    }

    private void AddState(CharacterState state)
    {
        _states[state.GetType()] = state;
    }

    public T GetState<T>() where T : CharacterState
    {
        return (T)_states[typeof(T)];
    }

    public void SetState<T>() where T : CharacterState
    {
        var newState = GetState<T>();
        if (CurrentState == newState)
            return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}