using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeedForRun = 2.5f;
    [SerializeField]
    private float horizontalSpeedForWalk = 0.5f;

    private Animator _animator;

    private static readonly int Run = Animator.StringToHash("isRun");
    private static readonly int Walk = Animator.StringToHash("isWalk");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");
    private static readonly int HasWeapon = Animator.StringToHash("hasWeapon");

    private static int WeaponLayer;
    
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
    }

    public void UpdateSpeed(float speed)
    {
        if (speed > horizontalSpeedForRun)
        {
            _animator.SetBool(Run, true);
            return;
        }
        else
        {
            _animator.SetBool(Run, false);
        }
        
        if (speed > horizontalSpeedForWalk)
        {
            _animator.SetBool(Walk, true);
        }
        else
        {
            _animator.SetBool(Walk, false);
        }
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
}