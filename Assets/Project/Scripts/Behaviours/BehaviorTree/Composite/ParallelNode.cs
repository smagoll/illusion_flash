using UnityEngine;

[CreateAssetMenu(fileName = "New Parallel", menuName = "BehaviourTree/Composite/Parallel")]
public class ParallelNode : CompositeNode
{
    [SerializeField] private int requiredSuccesses = 1;
    [SerializeField] private bool failOnFirst = false;
    
    public override NodeState Tick(Character character)
    {
        int successCount = 0;
        int failureCount = 0;
        int runningCount = 0;
        
        foreach (var child in children)
        {
            if (child == null) continue;
            
            var state = child.Tick(character);
            switch (state)
            {
                case NodeState.Success:
                    successCount++;
                    break;
                case NodeState.Failure:
                    failureCount++;
                    if (failOnFirst) return NodeState.Failure;
                    break;
                case NodeState.Running:
                    runningCount++;
                    break;
            }
        }
        
        if (successCount >= requiredSuccesses)
            return NodeState.Success;
        
        if (runningCount > 0)
            return NodeState.Running;
            
        return NodeState.Failure;
    }
}