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
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public abstract void SetSpeedType(MovementSpeedType speedType);
    public virtual void HandleRotation() { }
}