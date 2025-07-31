using UnityEngine;

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
        if (rootNode == null) return NodeState.Failure;
        return rootNode.Tick(character);
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