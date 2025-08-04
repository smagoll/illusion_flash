using UnityEngine;

public class AttackController
{
    private AnimationController _animationController;
    private WeaponController _weaponController;
    private MovementController _movementController;
    
    private bool isAttacking;
    
    public AttackController(AnimationController animationController, WeaponController weaponController, MovementController movementController)
    {
        _animationController = animationController;
        _weaponController = weaponController;
        _movementController = movementController;
        
        RegisterEvents();
    }

    public void Attack()
    {
        if (isAttacking) return;
        if (!_weaponController.IsWeaponDrawn) return;
        
        isAttacking = true;
        _animationController.Attack();
        _movementController.StopMove();
        
        Debug.Log("attack");
    }

    private void EndAttack()
    {
        isAttacking = false;
        _movementController.ResumeMove();
    }

    private void Impulse()
    {
        _movementController.ApplyImpulse(_movementController.Forward, 5f);
    }

    private void RegisterEvents()
    {
        _animationController.ModelEventsHandler.OnEndAttack += EndAttack;
        _animationController.ModelEventsHandler.OnImpulse += Impulse;
    }
}