using UnityEngine;

public class AttackController
{
    private AnimationController _animationController;
    private WeaponController _weaponController;
    
    private bool isAttacking = false;
    
    public AttackController(AnimationController animationController, WeaponController weaponController)
    {
        _animationController = animationController;
        _weaponController = weaponController;
        
        RegisterEvents();
    }

    public void Attack()
    {
        if (isAttacking) return;
        if (!_weaponController.IsWeaponDrawn) return;
        
        isAttacking = true;
        _animationController.Attack();
        
        Debug.Log("attack");
    }

    private void EndAttack()
    {
        isAttacking = false;
    }

    private void RegisterEvents()
    {
        _animationController.ModelEventsHandler.OnEndAttack += EndAttack;
    }
}