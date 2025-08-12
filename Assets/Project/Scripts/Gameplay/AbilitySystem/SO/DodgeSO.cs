using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dodge")]
public class DodgeSO : AbilitySO
{
    public override Ability Create()
    {
        return new DodgeAbility(Id);
    }
}