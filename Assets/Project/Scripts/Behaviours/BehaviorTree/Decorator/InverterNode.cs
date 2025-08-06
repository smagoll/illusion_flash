using UnityEngine;

[CreateAssetMenu(fileName = "New Inverter", menuName = "BehaviourTree/Decorator/Inverter")]
public class InverterNode : DecoratorNode
{
    protected override NodeState Tick(Character character)
    {
        if (child == null) return NodeState.Failure;
        
        var result = child.TickNode(character);
        switch (result)
        {
            case NodeState.Success: return NodeState.Failure;
            case NodeState.Failure: return NodeState.Success;
            default: return result;
        }
    }
}