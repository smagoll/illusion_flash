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
        if (other.gameObject == _weaponController.Character.gameObject)
            return;
        
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_weaponController.GetDamage());
            
            
            Vector3 hitPosition = other.ClosestPoint(transform.position);

            VFXSystem.Instance.SpawnImpact(
                VFXSystem.Instance.library.swordImpact,
                hitPosition,
                Vector3.zero
            );
        }
    }
}
