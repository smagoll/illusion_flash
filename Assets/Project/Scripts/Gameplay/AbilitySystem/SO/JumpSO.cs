using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Jump")]
public class JumpSO : AbilitySO
{
    [SerializeField] private float jumpForce;
    
    public override Ability Create()
    {
        return new JumpAbility(Id, jumpForce);
    }
}