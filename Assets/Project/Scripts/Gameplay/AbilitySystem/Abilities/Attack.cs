using UnityEngine;

public class AttackAbility : Ability
{
    private AnimationController _animationController;
    private WeaponController _weaponController;
    private MovementController _movementController;

    private bool _isAttacking;
    
    public override bool IsFinished => !_isAttacking;

    public AttackAbility(string id) : base(id)
    {
        
    }
    
    public override void Initialize(Character character)
    {
        base.Initialize(character);
        
        _animationController = character.AnimationController;
        _weaponController = character.WeaponController;
        _movementController = character.MovementController;

        RegisterEvents();
    }

    public override bool CanExecute()
    {
        return !_isAttacking && _weaponController is { IsWeaponDrawn: true } ;
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        _isAttacking = true;
        _animationController.Attack();
        _movementController.StopMove();
    }

    public override void Cleanup(Character character)
    {
        base.Cleanup(character);
        
        UnregisterEvents();
    }

    private void EndAttack()
    {
        _isAttacking = false;
        _movementController.ResumeMove();
    }

    private void Impulse()
    {
        _movementController.ApplyImpulse(_movementController.Forward, 5f);
    }

    private void RegisterEvents()
    {
        _animationController.ModelEventsHandler.OnEndAttack += EndAttack;
        _animationController.ModelEventsHandler.OnImpulse += Impulse;
    }

    private void UnregisterEvents()
    {
        if (_animationController != null && _animationController.ModelEventsHandler != null)
        {
            _animationController.ModelEventsHandler.OnEndAttack -= EndAttack;
            _animationController.ModelEventsHandler.OnImpulse -= Impulse;
        }
    }
}