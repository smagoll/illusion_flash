using System;

public class ConditionNode : Node
{
    private Func<Character, bool> _condition;
    public ConditionNode(Func<Character, bool> condition) => _condition = condition;

    public override NodeState Tick(Character character)
        => _condition(character) ? NodeState.Success : NodeState.Failure;
}