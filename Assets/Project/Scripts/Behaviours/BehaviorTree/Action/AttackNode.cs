using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Action/Attack")]
public class AttackNode : ActionNode
{
    public override NodeState ExecuteAction(Character character)
    {
        var bb = character.Blackboard;
        var abilityController = character.AbilityController;
        
        bool started = bb.GetValue(BBKeys.AttackStarted);
        
        if (!started)
        {
            if (!abilityController.TryExecute(AbilityKeys.Attack))
                return NodeState.Failure;

            bb.SetValue(BBKeys.AttackStarted, true);
            return NodeState.Running;
        }
        
        if (abilityController.IsPerformingAbility)
        {
            return NodeState.Running;
        }
        
        bb.SetValue(BBKeys.AttackStarted, false);
        return NodeState.Success;
    }
}