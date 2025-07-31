using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private static readonly int Speed = Animator.StringToHash("speed");

    public void UpdateSpeed(float speed)
    {
        animator.SetFloat(Speed, speed);
    }
}