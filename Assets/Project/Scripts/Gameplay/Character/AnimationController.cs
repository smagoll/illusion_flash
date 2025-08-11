using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeedForRun = 2.5f;
    [SerializeField]
    private float horizontalSpeedForWalk = 0.5f;

    private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Run = Animator.StringToHash("isRun");
    private static readonly int Walk = Animator.StringToHash("isWalk");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");
    private static readonly int HasWeapon = Animator.StringToHash("hasWeapon");
    private static readonly int Right = Animator.StringToHash("right");
    private static readonly int Forward = Animator.StringToHash("forward");

    public static int WeaponLayer;
    public static int LockOn;
    
    public ModelEventsHandler ModelEventsHandler { get; private set; }

    public void Init(Animator animator, ModelEventsHandler modelEventsHandler)
    {
        _animator = animator;
        ModelEventsHandler = modelEventsHandler;
        
        Setup();
    }
    
    private void Setup()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(Walk, false);
        
        WeaponLayer =  _animator.GetLayerIndex("Weapon Layer");
        LockOn =  _animator.GetLayerIndex("LockOn");
    }

    public void UpdateSpeed(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void UpdateDirection(Vector2 direction)
    {
        _animator.SetFloat(Right, direction.y);
        _animator.SetFloat(Forward, direction.x);
    }

    public void Jump()
    {
        _animator.SetTrigger(JumpTrigger);
    }
    
    public void Attack()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    public void UpdateIsFalling(bool isFalling)
    {
        _animator.SetBool(IsFalling, isFalling);
    }

    public void EquipWeapon()
    {
        //_animator.SetLayerWeight(WeaponLayer, 1f);
        _animator.SetBool(HasWeapon, true);
        _animator.SetTrigger("equipWeapon");
    }
    
    public void UnequipWeapon()
    {
        //_animator.SetLayerWeight(WeaponLayer, 0f);
        _animator.SetBool(HasWeapon, false);
        _animator.SetTrigger("unequipWeapon");
    }

    public void Death()
    {
        _animator.SetTrigger("death");
    }

    public void SetWeightLayer(int layer, float weight)
    {
        _animator.SetLayerWeight(layer, weight);
    }

    public void EnableDisableLockOn(bool isLockOn)
    {
        _animator.SetBool("isLockOn", isLockOn);
    }
}