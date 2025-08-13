using UnityEngine;

public abstract class MovementState
{
    protected readonly MovementController _controller;
    protected readonly MovementStateMachine _stateMachine;

    protected MovementState(MovementStateMachine stateMachine, MovementController controller)
    {
        _controller = controller;
        _stateMachine = stateMachine;
    }

    public abstract void Walk(Vector2 input);
    public abstract void NormalRun(Vector2 input);
    public abstract void Run(Vector2 input);
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public virtual void HandleMovement(Vector2 input) { }
    public virtual void HandleRotation() { }
}