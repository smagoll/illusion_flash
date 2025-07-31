public abstract class ConditionNode : BTNode
{
    public abstract bool CheckCondition(Character character);
    
    public override NodeState Tick(Character character)
    {
        return CheckCondition(character) ? NodeState.Success : NodeState.Failure;
    }
}