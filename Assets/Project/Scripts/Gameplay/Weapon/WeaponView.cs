using System;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    private WeaponController _weaponController;
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _collider.isTrigger = true;
    }

    public void Init(WeaponController weaponController)
    {
        _weaponController = weaponController;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_weaponController.GetDamage());
            
            VFXSystem.Instance.SpawnImpact(VFXSystem.Instance.library.swordImpact, transform.position, Vector3.zero);
        }
    }
}
