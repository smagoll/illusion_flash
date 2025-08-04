using System;
using UnityEngine;

public class ModelEventsHandler : MonoBehaviour
{
    public event Action OnWeaponHitboxEnabled;
    public event Action OnWeaponHitboxDisabled;
    
    public void EnableWeaponHitbox()
    {
        OnWeaponHitboxEnabled?.Invoke();
    }

    public void DisableWeaponHitbox()
    {
        OnWeaponHitboxDisabled?.Invoke();
    }
}
