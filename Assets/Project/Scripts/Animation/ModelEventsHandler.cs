using System;
using UnityEngine;

public class ModelEventsHandler : MonoBehaviour
{
    public event Action OnWeaponHitboxEnabled;
    public event Action OnWeaponHitboxDisabled;
    public event Action OnEquipWeapon;
    public event Action OnUnequipWeapon;
    public event Action OnEndAttack;
    
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
}
