public abstract class ActionNode : BTNode
{
    public abstract NodeState ExecuteAction(Character character);
    
    protected override NodeState Tick(Character character)
    {
        return ExecuteAction(character);
    }
}