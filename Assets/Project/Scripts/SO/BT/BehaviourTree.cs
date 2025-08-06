using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Behaviour Tree", menuName = "BehaviourTree/Behaviour Tree")]
public class BehaviourTree : ScriptableObject
{
    [SerializeField] private BTNode rootNode;
    
    public BTNode RootNode => rootNode;
    
    public void SetRootNode(BTNode node)
    {
        rootNode = node;
    }
    
    public NodeState Tick(Character character)
    {
        if (rootNode == null || character?.Blackboard == null) return NodeState.Failure;
        
        InitializeBTState(character);
        
        return rootNode.TickNode(character);
    }
    
    private void InitializeBTState(Character character)
    {
        if (!character.Blackboard.HasValue(BBKeys.RunningNodesKey))
        {
            character.Blackboard.SetValue(BBKeys.RunningNodesKey, new HashSet<int>());
        }
    }
    
    public void StopAll(Character character)
    {
        if (character?.Blackboard == null) return;
        
        var runningNodes = character.Blackboard.GetValue(BBKeys.RunningNodesKey);
        if (runningNodes == null) return;
        
        var nodesToStop = new List<int>(runningNodes);
        
        foreach (var nodeId in nodesToStop)
        {
            var node = FindNodeById(nodeId);
            if (node != null)
            {
                node.OnStop(character);
            }
        }
        
        runningNodes.Clear();
    }
    
    public void StopNode(BTNode node, Character character)
    {
        if (character?.Blackboard == null || node == null) return;
        
        var runningNodes = character.Blackboard.GetValue(BBKeys.RunningNodesKey);
        if (runningNodes == null || !runningNodes.Contains(node.GetInstanceID())) return;
        
        StopNodeRecursive(node, character);
    }
    
    private void StopNodeRecursive(BTNode node, Character character)
    {
        if (node == null) return;
        
        var runningNodes = character.Blackboard.GetValue(BBKeys.RunningNodesKey);
        int nodeId = node.GetInstanceID();
        
        if (runningNodes.Contains(nodeId))
        {
            node.OnStop(character);
            runningNodes.Remove(nodeId);
        }
        
        if (node is CompositeNode composite)
        {
            foreach (var child in composite.Children)
            {
                if (child != null)
                    StopNodeRecursive(child, character);
            }
        }
        else if (node is DecoratorNode decorator && decorator.Child != null)
        {
            StopNodeRecursive(decorator.Child, character);
        }
    }
    
    private BTNode FindNodeById(int nodeId)
    {
        return FindNodeByIdRecursive(rootNode, nodeId);
    }
    
    private BTNode FindNodeByIdRecursive(BTNode node, int targetId)
    {
        if (node == null) return null;
        if (node.GetInstanceID() == targetId) return node;
        
        if (node is CompositeNode composite)
        {
            foreach (var child in composite.Children)
            {
                var found = FindNodeByIdRecursive(child, targetId);
                if (found != null) return found;
            }
        }
        else if (node is DecoratorNode decorator && decorator.Child != null)
        {
            return FindNodeByIdRecursive(decorator.Child, targetId);
        }
        
        return null;
    }
    
    public void DrawGizmos()
    {
        if (rootNode == null) return;
        DrawNodeGizmos(rootNode);
    }
    
    private void DrawNodeGizmos(BTNode node)
    {
        if (node == null) return;
        
        node.OnDrawGizmos();
        
        if (node is CompositeNode composite)
        {
            foreach (var child in composite.Children)
            {
                DrawNodeGizmos(child);
            }
        }
        else if (node is DecoratorNode decorator && decorator.Child != null)
        {
            DrawNodeGizmos(decorator.Child);
        }
    }
}