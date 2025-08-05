using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Condition/Has weapon in hand")]
public class HasWeaponInHandNode : ConditionNode
{
    public override bool CheckCondition(Character character)
    {
        return character.WeaponController.IsWeaponDrawn;
    }
}