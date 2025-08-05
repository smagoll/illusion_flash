using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Condition/Player is alive")]
public class PlayerIsAliveNode : ConditionNode
{
    public override bool CheckCondition(Character character)
    {
        var player = character.Blackboard.GlobalBlackboard.GetValue(BBKeys.PlayerCharacter);

        return !player.Model.IsDeath;
    }
}