using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Action/Equip weapon")]
public class EquipWeaponNode : ActionNode
{
    public override NodeState ExecuteAction(Character character)
    {
        character.WeaponController.DrawWeapon();
        
        if (!character.WeaponController.IsWeaponDrawn)
        {
            return NodeState.Running;
        }
        
        return NodeState.Success;
    }
}