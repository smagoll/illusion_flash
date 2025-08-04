using UnityEngine;

public class AttackController
{
    private AnimationController _animationController;
    
    public AttackController(AnimationController animationController)
    {
        _animationController = animationController;
    }

    public void Attack()
    {
        _animationController.Attack();
        
        Debug.Log("attack");
    }
}