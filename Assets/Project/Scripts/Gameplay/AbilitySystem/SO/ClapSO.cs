using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Clap")]
public class ClapSO : AbilitySO
{
    [SerializeField] int healAmount = 30;
    
    public override Ability Create()
    {
        return new ClapHealAbility(Id, healAmount);
    }
}