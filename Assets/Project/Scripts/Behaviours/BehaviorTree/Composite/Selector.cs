using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> _children;
    public Selector(List<Node> children) => _children = children;

    public override NodeState Tick(Character character)
    {
        foreach (var child in _children)
        {
            var state = child.Tick(character);
            if (state != NodeState.Failure) return state;
        }
        return NodeState.Failure;
    }
}