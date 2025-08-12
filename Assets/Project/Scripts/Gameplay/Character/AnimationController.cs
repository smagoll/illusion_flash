using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Run = Animator.StringToHash("isRun");
    private static readonly int Walk = Animator.StringToHash("isWalk");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");
    private static readonly int DodgeTrigger = Animator.StringToHash("dodge");
    private static readonly int HasWeapon = Animator.StringToHash("hasWeapon");
    private static readonly int Right = Animator.StringToHash("right");
    private static readonly int Forward = Animator.StringToHash("forward");
    private static readonly int IsDeath = Animator.StringToHash("isDeath");
    private static readonly int EquipWeapon1 = Animator.StringToHash("equipWeapon");
    private static readonly int UnequipWeapon1 = Animator.StringToHash("unequipWeapon");

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

    public void Dodge()
    {
        _animator.SetTrigger(DodgeTrigger);
    }

    public void UpdateIsFalling(bool isFalling)
    {
        _animator.SetBool(IsFalling, isFalling);
    }

    public void EquipWeapon()
    {
        _animator.SetBool(HasWeapon, true);
        _animator.SetTrigger(EquipWeapon1);
    }
    
    public void UnequipWeapon()
    {
        _animator.SetBool(HasWeapon, false);
        _animator.SetTrigger(UnequipWeapon1);
    }

    public void Death()
    {
        SetWeightLayer(WeaponLayer, 0f);
        _animator.SetBool(IsDeath, true);
    }

    private void SetWeightLayer(int layer, float weight)
    {
        _animator.SetLayerWeight(layer, weight);
    }

    public void EnableDisableLockOn(bool isLockOn)
    {
        _animator.SetBool("isLockOn", isLockOn);
    }
}