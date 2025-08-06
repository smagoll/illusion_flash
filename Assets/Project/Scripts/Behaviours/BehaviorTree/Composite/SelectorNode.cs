using UnityEngine;

[CreateAssetMenu(fileName = "New Selector", menuName = "BehaviourTree/Composite/Selector")]
public class SelectorNode : CompositeNode
{
    protected override NodeState Tick(Character character)
    {
        foreach (var child in children)
        {
            if (child == null) continue;
            
            var state = child.TickNode(character);
            if (state != NodeState.Failure) 
                return state;
        }
        return NodeState.Failure;
    }
}