using UnityEngine;

[CreateAssetMenu(fileName = "New Move To Target", menuName = "BehaviourTree/Action/Move To Target")]
public class MoveToTargetNode : ActionNode
{
    [SerializeField] private float stopDistance = 1f;

    public override void OnStart(Character character)
    {
        character.MovementController.ResumeMove();
    }

    public override NodeState ExecuteAction(Character character)
    {
        var target = character.Blackboard.GlobalBlackboard.GetValue(BBKeys.PlayerCharacter).gameObject.transform;
        if (target == null) return NodeState.Failure;
        
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;
        
        if (distance <= stopDistance)
        {
            return NodeState.Success;
        }
        
        direction.Normalize();
        
        character.MovementController.MoveTo(target.position, 5f);
        
        return NodeState.Running;
    }

    public override void OnStop(Character character)
    {
        character.MovementController.StopMove();
    }
}