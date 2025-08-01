using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private float horizontalSpeedForRun = 2.5f;

    private static readonly int Run = Animator.StringToHash("isRun");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");

    private void Start()
    {
        animator.SetBool(Run, false);
    }

    public void UpdateSpeed(float speed)
    {
        if (speed > horizontalSpeedForRun)
        {
            animator.SetBool(Run, true);
        }
        else
        {
            animator.SetBool(Run, false);
        }
    }

    public void Jump()
    {
        animator.SetTrigger(JumpTrigger);
    }
}