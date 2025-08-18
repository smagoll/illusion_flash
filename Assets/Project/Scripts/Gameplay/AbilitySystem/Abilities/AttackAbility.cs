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
        if (_isAttacking) return false;
        if (_weaponController?.IsWeaponDrawn != true) return false;
        
        var attack = _weaponController.ComboSystem.GetCurrentAttack();
        if (attack == null) return false;
        
        return Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        if (!CanExecute()) return;
        
        var success = Character.StateMachine.TrySetState<CharacterAttackState>();
        
        if (success)
        {
            UseAttack();
        }
        else
        {
            if (Character.StateMachine.CurrentState is CharacterAttackState attackState)
            {
                if (attackState.TryNextAttack())
                {
                    attackState.HandleAttack();
                    UseAttack();
                }
            }
        }
    }

    private void UseAttack()
    {
        Character.Model.UseStamina(_stamina);
    }
}