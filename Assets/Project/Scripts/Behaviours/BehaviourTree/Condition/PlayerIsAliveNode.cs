public class PlayerIsAliveNode : CharacterConditionBase
{
    protected override bool OnCheck()
    {
        return !Player.Model.IsDeath;
    }
}