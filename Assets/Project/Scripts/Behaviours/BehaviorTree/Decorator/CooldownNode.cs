using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cooldown", menuName = "BehaviourTree/Decorator/Cooldown")]
public class CooldownNode : DecoratorNode
{
    [SerializeField] private float cooldownTime = 1f;
    
    private Dictionary<Character, float> lastExecutionTimes = new Dictionary<Character, float>();
    
    protected override NodeState Tick(Character character)
    {
        if (child == null) return NodeState.Failure;
        
        float currentTime = Time.time;
        
        if (lastExecutionTimes.TryGetValue(character, out float lastTime))
        {
            if (currentTime - lastTime < cooldownTime)
                return NodeState.Failure;
        }
        
        var result = child.TickNode(character);
        
        if (result == NodeState.Success)
        {
            lastExecutionTimes[character] = currentTime;
        }
        
        return result;
    }
}