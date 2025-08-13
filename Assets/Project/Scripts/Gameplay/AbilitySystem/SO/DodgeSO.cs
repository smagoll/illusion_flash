using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dodge")]
public class DodgeSO : AbilitySO
{
    [SerializeField] private int stamina = 10;
    
    public override Ability Create()
    {
        return new DodgeAbility(Id, stamina);
    }
}