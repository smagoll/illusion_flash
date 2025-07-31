using UnityEngine;

public enum NodeState { Success, Failure, Running }

// Базовый абстрактный класс для всех нод
public abstract class BTNode : ScriptableObject
{
    [SerializeField] protected string nodeName = "Node";
    [SerializeField] protected string description = "";
    
    public string NodeName => nodeName;
    public string Description => description;
    
    public abstract NodeState Tick(Character character);
    
    // Для отладки
    public virtual void OnDrawGizmos() { }
}