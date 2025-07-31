using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> _children;
    public Sequence(List<Node> children) => _children = children;

    public override NodeState Tick(Character character)
    {
        foreach (var child in _children)
        {
            var state = child.Tick(character);
            if (state != NodeState.Success) return state;
        }
        return NodeState.Success;
    }
}