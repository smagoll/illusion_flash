using UnityEngine;

public class AttackAbility : Ability
{
    private WeaponController _weaponController;

    private float _stamina;
    private bool _isAttacking;
    
    public override bool IsFinished => !_isAttacking;

    public AttackAbility(string id, float stamina) : base(id)
    {
        _stamina = stamina;
    }
    
    public override void Initialize(Character character)
    {
        base.Initialize(character);
        
        _weaponController = character.WeaponController;
    }

    public override bool CanExecute()
    {
        return !_isAttacking && _weaponController is { IsWeaponDrawn: true } && Character.Model.Stamina.Current >= _stamina && !Character.StateMachine.IsState<CharacterAttackState>();
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        Character.StateMachine.SetState<CharacterAttackState>();
        
        Character.Model.UseStamina(_stamina);
    }
}