using UnityEngine;

public class AttackController : MonoBehaviour
{
    private AnimationController _animationController;
    
    public void Init(AnimationController animationController)
    {
        _animationController = animationController;
    }

    public void Attack()
    {
        _animationController.Attack();
        
        Debug.Log("attack");
    }
}