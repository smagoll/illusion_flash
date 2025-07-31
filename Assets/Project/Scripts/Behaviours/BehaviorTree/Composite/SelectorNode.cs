using UnityEngine;

[CreateAssetMenu(fileName = "New Selector", menuName = "BehaviourTree/Composite/Selector")]
public class SelectorNode : CompositeNode
{
    public override NodeState Tick(Character character)
    {
        foreach (var child in children)
        {
            if (child == null) continue;
            
            var state = child.Tick(character);
            if (state != NodeState.Failure) 
                return state;
        }
        return NodeState.Failure;
    }
}