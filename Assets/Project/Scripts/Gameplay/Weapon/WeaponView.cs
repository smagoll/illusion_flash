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
            var damageData = _weaponController.GetDamage();

            var hitPosition = other.ClosestPoint(transform.position);
            Vector3 hitNormal = (hitPosition - other.bounds.center).normalized;
            
            damageData.SetHit(hitPosition, hitNormal);
            damageable.TakeDamage(damageData);
        }
    }
}
