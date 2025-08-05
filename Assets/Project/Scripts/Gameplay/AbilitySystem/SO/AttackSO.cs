using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Attack")]
public class AttackSO : AbilitySO
{
    public override Ability Create()
    {
        return new AttackAbility(Id);
    }
}