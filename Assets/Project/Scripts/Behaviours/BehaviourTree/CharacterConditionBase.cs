using NodeCanvas.Framework;

public abstract class CharacterConditionBase : ConditionTask
{
    protected Character Character { get; set; }
    protected Character Player { get; set; }

    protected override string OnInit()
    {
        Player = blackboard.GetVariableValue<IGlobalBlackboard>(BBKeys.GlobalBlackboard).GetVariableValue<Character>("PlayerCharacter");
        Character = blackboard.GetVariableValue<Character>("PlayerCharacter");
        
        if (Character == null)
            return "Character is null";

        return null;
    }
}