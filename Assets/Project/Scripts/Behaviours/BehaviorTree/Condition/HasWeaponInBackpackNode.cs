using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Condition/Has weapon in backpack")]
public class HasWeaponInBackpackNode : ConditionNode
{
    public override bool CheckCondition(Character character)
    {
        return character.WeaponController.IsWeapon;
    }
}