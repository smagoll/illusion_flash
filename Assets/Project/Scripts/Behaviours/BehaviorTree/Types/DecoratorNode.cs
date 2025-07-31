using UnityEngine;

public abstract class DecoratorNode : BTNode
{
    [SerializeField] protected BTNode child;
    
    public BTNode Child => child;
    
    public void SetChild(BTNode newChild)
    {
        child = newChild;
    }
}