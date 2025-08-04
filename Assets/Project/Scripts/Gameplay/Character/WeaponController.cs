using UnityEngine;

public class WeaponController
{
    private AnimationController _animationController;
    private SocketHolder _socketHolder;
    private ModelEventsHandler _modelModelEventsHandler;
    
    private WeaponView _currentWeaponGO;
    private Weapon _currentWeapon;

    public bool IsWeaponDrawn { get; private set; }

    public WeaponController(AnimationController animationController, SocketHolder socketHolder, ModelEventsHandler modelEventsHandler)
    {
        _animationController = animationController;
        _socketHolder = socketHolder;
        _modelModelEventsHandler = modelEventsHandler;
        
        RegisterEvents();
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void DrawWeapon()
    {
        if (_currentWeapon == null || IsWeaponDrawn) return;
        
        _animationController.EquipWeapon();
    }

    private void CreateWeapon()
    {
        _currentWeaponGO = Object.Instantiate(_currentWeapon.Prefab, _socketHolder.weapon);
        _currentWeaponGO.transform.localPosition = Vector3.zero;
        _currentWeaponGO.transform.localRotation = Quaternion.identity;
        _currentWeaponGO.Init(this);
        
        IsWeaponDrawn = true;
    }

    private void DestroyWeapon()
    {
        if (_currentWeaponGO != null)
            Object.Destroy(_currentWeaponGO.gameObject);
        
        IsWeaponDrawn = false;
    }

    private void SheatheWeapon()
    {
        if (!IsWeaponDrawn) return;
        
        _animationController.UnequipWeapon();
    }

    private void EnableHitbox()
    {
        _currentWeaponGO.gameObject.GetComponent<Collider>().enabled = true;
    }
    
    private void DisableHitbox()
    {
        _currentWeaponGO.gameObject.GetComponent<Collider>().enabled = false;
    }

    public void ToggleWeapon()
    {
        if (IsWeaponDrawn)
            SheatheWeapon();
        else
            DrawWeapon();
    }

    private void RegisterEvents()
    {
        _modelModelEventsHandler.OnWeaponHitboxEnabled += EnableHitbox;
        _modelModelEventsHandler.OnWeaponHitboxDisabled += DisableHitbox;
        _modelModelEventsHandler.OnEquipWeapon += CreateWeapon;
        _modelModelEventsHandler.OnUnequipWeapon += DestroyWeapon;
    }

    public int GetDamage()
    {
        return _currentWeapon.Damage;
    }
}