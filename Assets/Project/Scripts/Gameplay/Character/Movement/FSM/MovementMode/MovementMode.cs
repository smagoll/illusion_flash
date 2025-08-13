using UnityEngine;

public abstract class MovementMode
{
    protected readonly MovementController _controller;

    protected MovementMode(MovementController controller)
    {
        _controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public virtual void HandleMovement(Vector2 input) { }
    public virtual void HandleRotation() { }
}