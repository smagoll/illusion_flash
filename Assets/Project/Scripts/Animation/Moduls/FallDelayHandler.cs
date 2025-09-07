using UnityEngine;

public class FallDelayHandler
{
    private readonly float _fallDelay;
    private float _fallTimer;
    private bool _isFalling;

    private readonly Animator _animator;
    private readonly int _isFallingHash;

    public FallDelayHandler(Animator animator, string fallingParam = "isFalling", float fallDelay = 0.25f)
    {
        _animator = animator;
        _fallDelay = fallDelay;
        _isFallingHash = Animator.StringToHash(fallingParam);
    }

    /// <summary>
    /// Обновляет состояние падения и управляет анимацией
    /// </summary>
    public void UpdateFallState(bool isGrounded, float verticalSpeed)
    {
        if (isGrounded)
        {
            _fallTimer = 0f;
            _isFalling = false;
        }
        else
        {
            if (verticalSpeed < -1f)
            {
                _fallTimer += Time.deltaTime;
                if (_fallTimer >= _fallDelay)
                {
                    _isFalling = true;
                }
            }
        }

        _animator.SetBool(_isFallingHash, _isFalling);
    }
}