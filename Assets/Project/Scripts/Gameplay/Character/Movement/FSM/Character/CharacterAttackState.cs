using UnityEngine;

public class CharacterAttackState : CharacterState
{
    private bool _isAttackFinished;

    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Attack();
        
        _character.AnimationController.ModelEventsHandler.OnEndAttack += OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse += OnImpulse;
    }

    public void Attack()
    {
        var attack = _character.WeaponController.ComboSystem.GetCurrentAttack();
        
        if (attack == null)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
            return;
        }
        
        Debug.Log(attack.AnimationName);
        
        _character.AnimationController.Attack(attack.AnimationName);
        _character.WeaponController.ComboSystem.OnAttack();
        _isAttackFinished = false;
    }

    private void OnAttackFinished()
    {
        _isAttackFinished = true;
    }
    
    private void OnImpulse()
    {
        Vector3 forward = _character.MovementController.Forward;
        _character.MovementController.ApplyImpulse(forward, strength: 5f);
    }

    public override void Update()
    {
        _character.WeaponController.ComboSystem.AllowNext();
        
        if (_isAttackFinished)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public bool TryNextAttack()
    {
        return _character.WeaponController.ComboSystem.CanContinue;
    }

    public override void Exit()
    {
        _character.AnimationController.ModelEventsHandler.OnEndAttack -= OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse -= OnImpulse;
        
        _character.WeaponController.ComboSystem.ResetCombo();
    }
}