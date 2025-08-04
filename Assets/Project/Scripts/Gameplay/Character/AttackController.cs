using UnityEngine;

public class AttackController
{
    private WeaponController _weaponController;
    private AnimationController _animationController;
    
    public AttackController(AnimationController animationController, WeaponController weaponController)
    {
        _animationController = animationController;
        _weaponController = weaponController;
    }

    public void Attack()
    {
        _animationController.Attack();
        
        Debug.Log("attack");
    }
}