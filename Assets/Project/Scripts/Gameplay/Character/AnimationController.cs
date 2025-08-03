using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private float horizontalSpeedForRun = 2.5f;
    [SerializeField]
    private float horizontalSpeedForWalk = 0.5f;

    private static readonly int Run = Animator.StringToHash("isRun");
    private static readonly int Walk = Animator.StringToHash("isWalk");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");

    private void Start()
    {
        animator.SetBool(Run, false);
        animator.SetBool(Walk, false);
    }

    public void UpdateSpeed(float speed)
    {
        if (speed > horizontalSpeedForRun)
        {
            animator.SetBool(Run, true);
            return;
        }
        else
        {
            animator.SetBool(Run, false);
        }
        
        if (speed > horizontalSpeedForWalk)
        {
            animator.SetBool(Walk, true);
        }
        else
        {
            animator.SetBool(Walk, false);
        }
    }

    public void Jump()
    {
        animator.SetTrigger(JumpTrigger);
    }
    
    public void Attack()
    {
        animator.SetTrigger(AttackTrigger);
    }

    public void UpdateIsFalling(bool isFalling)
    {
        animator.SetBool(IsFalling, isFalling);
    }
}