using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Debug Log", menuName = "BehaviourTree/Action/Debug Log")]
public class DebugLogNode : ActionNode
{
    [SerializeField] private string message = "Debug message";
    [SerializeField] private bool logOnce = true;
    
    private HashSet<Character> loggedCharacters = new HashSet<Character>();
    
    public override NodeState ExecuteAction(Character character)
    {
        if (logOnce && loggedCharacters.Contains(character))
            return NodeState.Success;
        
        Debug.Log($"{character.name}: {message}");
        
        if (logOnce)
            loggedCharacters.Add(character);
        
        return NodeState.Success;
    }
}