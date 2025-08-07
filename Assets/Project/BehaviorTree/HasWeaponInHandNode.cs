public class HasWeaponInHandNode : CharacterConditionBase
{
    
    
    protected override bool OnCheck()
    {
        return Character.WeaponController.IsWeaponDrawn;
    }
}