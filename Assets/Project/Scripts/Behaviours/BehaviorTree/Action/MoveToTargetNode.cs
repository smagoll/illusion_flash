using UnityEngine;

[CreateAssetMenu(fileName = "New Move To Target", menuName = "BehaviourTree/Action/Move To Target")]
public class MoveToTargetNode : ActionNode
{
    [SerializeField] private float stopDistance = 1f;
    
    private Transform target;
    private Transform owner;
    
    public override NodeState ExecuteAction(Character character)
    {
        if (!owner) owner = character.transform;
        
        target = character.Blackboard.GlobalBlackboard.GetValue(BBKeys.PlayerCharacter).gameObject.transform;
        if (target == null) return NodeState.Failure;
        
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;
        
        if (distance <= stopDistance)
        {
            character.MovementController.StopMove();
            return NodeState.Success;
        }
        
        direction.Normalize();
        
        character.MovementController.MoveTo(target.position, 5f);
        
        return NodeState.Running;
    }
    
    public override void OnDrawGizmos()
    {
        if (owner != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.transform.position, owner.transform.position);
        }
    }
}