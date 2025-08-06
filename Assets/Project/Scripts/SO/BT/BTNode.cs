using UnityEngine;

public enum NodeState { Success, Failure, Running }

// Базовый абстрактный класс для всех нод
public abstract class BTNode : ScriptableObject
{
    [SerializeField] protected string nodeName = "Node";
    [SerializeField] protected string description = "";
    
    public string NodeName => nodeName;
    public string Description => description;
    
    public virtual void OnStart(Character character) { }
    public virtual void OnStop(Character character) { }
    public virtual void OnAbort(Character character) { }
    
    protected abstract NodeState Tick(Character character);
    public virtual void OnDrawGizmos() { }
    
    public NodeState TickNode(Character character)
    {
        var runningNodes = character.Blackboard.GetValue(BBKeys.RunningNodesKey);
        int nodeId = GetInstanceID();
        
        if (!runningNodes.Contains(nodeId))
        {
            OnStart(character);
            runningNodes.Add(nodeId);
        }
        
        NodeState state = Tick(character);
        
        if (state == NodeState.Success || state == NodeState.Failure)
        {
            if (runningNodes.Contains(nodeId))
            {
                OnStop(character);
                runningNodes.Remove(nodeId);
            }
        }
        
        return state;
    }
}