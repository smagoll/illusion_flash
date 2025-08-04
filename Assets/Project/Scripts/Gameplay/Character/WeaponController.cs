using UnityEngine;

public class WeaponController
{
    private AnimationController _animationController;
    private SocketHolder _socketHolder;
    
    private GameObject _currentWeaponGO;
    private Weapon _currentWeapon;

    public bool IsWeaponDrawn { get; private set; }

    public WeaponController(AnimationController animationController, SocketHolder socketHolder)
    {
        _animationController = animationController;
        _socketHolder = socketHolder;
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void DrawWeapon()
    {
        if (_currentWeapon == null || IsWeaponDrawn) return;

        _currentWeaponGO = Object.Instantiate(_currentWeapon.Prefab, _socketHolder.weapon);
        _currentWeaponGO.transform.localPosition = Vector3.zero;
        _currentWeaponGO.transform.localRotation = Quaternion.identity;
        
        _animationController.EquipWeapon();

        IsWeaponDrawn = true;
    }

    private void SheatheWeapon()
    {
        if (!IsWeaponDrawn) return;

        if (_currentWeaponGO != null)
            Object.Destroy(_currentWeaponGO);

        _animationController.UnequipWeapon();

        IsWeaponDrawn = false;
    }

    public void ToggleWeapon()
    {
        if (IsWeaponDrawn)
            SheatheWeapon();
        else
            DrawWeapon();
    }
}