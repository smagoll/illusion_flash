using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Condition/Is in attack range")]
public class IsInAttackRangeNode : ConditionNode
{
    public override bool CheckCondition(Character character)
    {
        var player = character.Blackboard.GlobalBlackboard.GetValue(BBKeys.PlayerCharacter);
        var target = player.gameObject.transform;
        
        if (target == null) return false;
        if (!character.WeaponController.IsWeapon) return false;
        
        float distance = Vector3.Distance(character.transform.position, target.position);
        
        Debug.Log($"distance: {distance} || range:{character.WeaponController.CurrentWeapon.Range}");
        
        return distance <= character.WeaponController.CurrentWeapon.Range;
    }
}