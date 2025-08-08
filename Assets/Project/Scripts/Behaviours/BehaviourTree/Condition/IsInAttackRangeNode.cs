using UnityEngine;

public class IsInAttackRangeNode : CharacterConditionBase
{
    protected override bool OnCheck()
    {
        var target = Player.transform;
        if (target == null) return false;
        if (!Character.WeaponController.IsWeapon) return false;

        float distance = Vector3.Distance(Character.transform.position, target.position);
        return distance <= Character.WeaponController.CurrentWeapon.Range;
    }
}