using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Attack")]
public class Attack : AbilitySO
{
    public override Ability Create()
    {
        return new AttackAbility(Id);
    }
}