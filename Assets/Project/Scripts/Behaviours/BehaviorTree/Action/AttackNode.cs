using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Action/Attack")]
public class AttackNode : ActionNode
{
    public override NodeState ExecuteAction(Character character)
    {
        return character.AbilityController.TryExecute(AbilityKeys.Attack) ? NodeState.Success : NodeState.Failure;
    }
}