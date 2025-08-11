public class IsAliveNode : CharacterConditionBase
{
    protected override bool OnCheck()
    {
        return !Character.Model.IsDeath;
    }
}