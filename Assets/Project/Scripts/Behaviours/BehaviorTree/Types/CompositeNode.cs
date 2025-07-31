using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : BTNode
{
    [SerializeField] protected List<BTNode> children = new List<BTNode>();
    
    public List<BTNode> Children => children;
    
    public void AddChild(BTNode child)
    {
        if (child != null && !children.Contains(child))
            children.Add(child);
    }
    
    public void RemoveChild(BTNode child)
    {
        children.Remove(child);
    }
    
    public void ClearChildren()
    {
        children.Clear();
    }
}