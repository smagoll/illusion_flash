using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Jump")]
public class JumpSO : AbilitySO
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float stamina;
    
    public override Ability Create()
    {
        return new JumpAbility(Id, jumpForce, stamina);
    }
}