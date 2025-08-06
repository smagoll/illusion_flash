using UnityEngine;

public abstract class MovementState
{
    protected MovementController controller;

    public MovementState(MovementController controller)
    {
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public virtual void HandleMovement(Vector2 input, float speed) { }
    public virtual void HandleRotation() { }
}