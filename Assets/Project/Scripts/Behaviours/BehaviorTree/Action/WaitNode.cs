using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wait", menuName = "BehaviourTree/Action/Wait")]
public class WaitNode : ActionNode
{
    [SerializeField] private float waitTime = 1f;
    
    private Dictionary<Character, float> startTimes = new Dictionary<Character, float>();
    
    public override NodeState ExecuteAction(Character character)
    {
        if (!startTimes.ContainsKey(character))
        {
            startTimes[character] = Time.time;
        }
        
        if (Time.time - startTimes[character] >= waitTime)
        {
            startTimes.Remove(character);
            return NodeState.Success;
        }
        
        return NodeState.Running;
    }
}