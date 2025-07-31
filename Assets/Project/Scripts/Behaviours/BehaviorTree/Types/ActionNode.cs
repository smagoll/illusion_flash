public abstract class ActionNode : BTNode
{
    public abstract NodeState ExecuteAction(Character character);
    
    public override NodeState Tick(Character character)
    {
        return ExecuteAction(character);
    }
}