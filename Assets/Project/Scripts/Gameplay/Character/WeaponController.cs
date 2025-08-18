using UnityEngine;

public class WeaponController
{
    private AnimationController _animationController;
    private SocketHolder _socketHolder;
    
    private WeaponView _currentWeaponGO;
    private Weapon _currentWeapon;
    private Collider _weaponCollider;

    public bool IsWeaponDrawn { get; private set; }
    public bool IsWeapon => _currentWeapon != null;

    public Weapon CurrentWeapon => _currentWeapon;
    public Character Character { get; private set; }

    public WeaponController(AnimationController animationController, SocketHolder socketHolder, Character character)
    {
        _animationController = animationController;
        _socketHolder = socketHolder;
        Character = character;
        
        RegisterEvents();
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        Character.CombatSystem.ComboSystem.SetCombo(weapon.Combo);
    }

    private void CreateWeapon()
    {
        _currentWeaponGO = Object.Instantiate(_currentWeapon.Prefab, _socketHolder.weapon);
        _currentWeaponGO.transform.localPosition = Vector3.zero;
        _currentWeaponGO.transform.localRotation = Quaternion.identity;
        _currentWeaponGO.Init(this);
        
        _weaponCollider = _currentWeaponGO.GetComponent<Collider>();
        IsWeaponDrawn = true;
    }

    private void DestroyWeapon()
    {
        if (_currentWeaponGO != null)
            Object.Destroy(_currentWeaponGO.gameObject);
        
        IsWeaponDrawn = false;
    }
    
    public void DrawWeapon()
    {
        if (_currentWeapon == null || IsWeaponDrawn) return;
        
        _animationController.UpdateEquippedWeapon(true);
    }

    public void SheatheWeapon()
    {
        if (!IsWeaponDrawn) return;
        
        _animationController.UpdateEquippedWeapon(false);
    }

    private void EnableHitbox() => _weaponCollider.enabled = true;
    private void DisableHitbox() => _weaponCollider.enabled = false;

    public void ToggleWeapon()
    {
        if (IsWeaponDrawn)
            SheatheWeapon();
        else
            DrawWeapon();
    }

    private void RegisterEvents()
    {
        _animationController.ModelEventsHandler.OnWeaponHitboxEnabled += EnableHitbox;
        _animationController.ModelEventsHandler.OnWeaponHitboxDisabled += DisableHitbox;
        _animationController.ModelEventsHandler.OnEquipWeapon += CreateWeapon;
        _animationController.ModelEventsHandler.OnUnequipWeapon += DestroyWeapon;
    }

    public DamageData GetDamage()
    {
        return new DamageData(_currentWeapon.Damage, Character);
    }
}