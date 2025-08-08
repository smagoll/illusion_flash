public class HasWeaponInBackpackNode : CharacterConditionBase
{
    protected override bool OnCheck()
    {
        return Character.WeaponController.IsWeapon;
    }
}