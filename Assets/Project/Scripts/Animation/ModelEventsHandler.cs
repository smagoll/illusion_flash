using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Animator))]
public class ModelEventsHandler : MonoBehaviour
{
    private Animator _animator;
    
    public event Action<Vector3, Quaternion> OnAnimatorMoveRoot;
    public event Action OnWeaponHitboxEnabled;
    public event Action OnWeaponHitboxDisabled;
    public event Action OnEquipWeapon;
    public event Action OnUnequipWeapon;
    public event Action OnEndAttack;
    public event Action OnEndDodge;
    public event Action OnImpulse;
    public event Action OnOpenComboWindow;
    public event Action OnCloseComboWindow;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnableWeaponHitbox()
    {
        OnWeaponHitboxEnabled?.Invoke();
    }

    public void DisableWeaponHitbox()
    {
        OnWeaponHitboxDisabled?.Invoke();
    }

    public void EquipWeapon()
    {
        OnEquipWeapon?.Invoke();
    }

    public void UnequipWeapon()
    {
        OnUnequipWeapon?.Invoke();
    }

    public void EndAttack()
    {
        OnEndAttack?.Invoke();
    }
    
    public void EndDodge()
    {
        OnEndDodge?.Invoke();
    }

    public void Impulse()
    {
        OnImpulse?.Invoke();
    }

    public void OnAnimatorMove()
    {
        OnAnimatorMoveRoot?.Invoke(_animator.deltaPosition, _animator.deltaRotation);
    }

    public void OpenCombo()
    {
        OnOpenComboWindow?.Invoke();
    }
    
    public void CloseCombo()
    {
        OnCloseComboWindow?.Invoke();
    }

    public void PlaySound(Object soundDataObject)
    {
        SoundData soundData = soundDataObject as SoundData;
        AudioSystem.Instance.Play(soundData.eventName, Vector3.zero);
    }
}
