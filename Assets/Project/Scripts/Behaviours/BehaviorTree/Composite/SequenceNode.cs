using UnityEngine;

[CreateAssetMenu(fileName = "New Sequence", menuName = "BehaviourTree/Composite/Sequence")]
public class SequenceNode : CompositeNode
{
    protected override NodeState Tick(Character character)
    {
        foreach (var child in children)
        {
            if (child == null) continue;
            
            var state = child.TickNode(character);
            if (state != NodeState.Success) 
                return state;
        }
        return NodeState.Success;
    }
}