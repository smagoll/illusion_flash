using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Attack")]
public class AttackSO : AbilitySO
{
    [SerializeField] private int stamina = 10;
    
    public override Ability Create()
    {
        return new AttackAbility(Id, stamina);
    }
}