using UnityEngine;

[CreateAssetMenu(fileName = "New Repeater", menuName = "BehaviourTree/Decorator/Repeater")]
public class RepeaterNode : DecoratorNode
{
    [SerializeField] private int maxRepeats = -1; // -1 для бесконечного повторения
    [SerializeField] private bool stopOnFailure = true;
    
    private int currentRepeats = 0;
    
    public override NodeState Tick(Character character)
    {
        if (child == null) return NodeState.Failure;
        
        if (maxRepeats > 0 && currentRepeats >= maxRepeats)
        {
            currentRepeats = 0;
            return NodeState.Success;
        }
        
        var result = child.Tick(character);
        
        if (result == NodeState.Success || (result == NodeState.Failure && !stopOnFailure))
        {
            currentRepeats++;
            return NodeState.Running;
        }
        
        if (result == NodeState.Failure && stopOnFailure)
        {
            currentRepeats = 0;
            return NodeState.Failure;
        }
        
        return result;
    }
}