using UnityEngine;

[CreateAssetMenu(fileName = "New Sequence", menuName = "BehaviourTree/Composite/Sequence")]
public class SequenceNode : CompositeNode
{
    public override NodeState Tick(Character character)
    {
        foreach (var child in children)
        {
            if (child == null) continue;
            
            var state = child.Tick(character);
            if (state != NodeState.Success) 
                return state;
        }
        return NodeState.Success;
    }
}