using UnityEngine;

[CreateAssetMenu(fileName = "New Move To Target", menuName = "BehaviourTree/Action/Move To Target")]
public class MoveToTargetNode : ActionNode
{
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private string targetTag = "Player";
    
    private Transform target;
    
    public override NodeState ExecuteAction(Character character)
    {
        target = GameObject.FindWithTag(targetTag).transform;
        if (target == null) return NodeState.Failure;
        
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;
        
        if (distance <= stopDistance)
        {
            character.Movement.Move(Vector2.zero);
            return NodeState.Success;
        }
        
        Vector2 moveDirection = new Vector2(direction.x, direction.z).normalized * moveSpeed;
        character.Movement.Move(moveDirection);
        
        return NodeState.Running;
    }
    
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.transform.position, stopDistance);
    }
}