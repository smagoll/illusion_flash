using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponSocket;

    private AnimationController _animationController;
    
    private GameObject _currentWeaponGO;
    private Weapon _currentWeapon;

    public bool IsWeaponDrawn { get; private set; }

    public void Init(AnimationController animationController)
    {
        _animationController = animationController;
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void DrawWeapon()
    {
        if (_currentWeapon == null || IsWeaponDrawn) return;

        _currentWeaponGO = Instantiate(_currentWeapon.Prefab, weaponSocket);
        _currentWeaponGO.transform.localPosition = Vector3.zero;
        _currentWeaponGO.transform.localRotation = Quaternion.identity;
        
        _animationController.EquipWeapon();

        IsWeaponDrawn = true;
    }

    private void SheatheWeapon()
    {
        if (!IsWeaponDrawn) return;

        if (_currentWeaponGO != null)
            Destroy(_currentWeaponGO);

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