using System;

public class ActionNode : Node
{
    private Func<Character, NodeState> _action;
    public ActionNode(Func<Character, NodeState> action) => _action = action;

    public override NodeState Tick(Character character) => _action(character);
}