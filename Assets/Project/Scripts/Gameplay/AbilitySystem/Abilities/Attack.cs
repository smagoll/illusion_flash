using UnityEngine;

public class AttackAbility : Ability
{
    private WeaponController _weaponController;

    private bool _isAttacking;
    
    public override bool IsFinished => !_isAttacking;

    public AttackAbility(string id) : base(id)
    {
        
    }
    
    public override void Initialize(Character character)
    {
        base.Initialize(character);
        
        _weaponController = character.WeaponController;
    }

    public override bool CanExecute()
    {
        return !_isAttacking && _weaponController is { IsWeaponDrawn: true } ;
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        Character.StateMachine.SetState<CharacterAttackState>();
        
        Character.Model.UseStamina(10);
    }
}