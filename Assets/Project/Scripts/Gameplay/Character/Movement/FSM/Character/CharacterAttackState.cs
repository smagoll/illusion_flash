using UnityEngine;

public class CharacterAttackState : CharacterState
{
    private bool _isAttackFinished;
    private bool _isAttackHandle;

    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Attack();
        
        _character.AnimationController.ModelEventsHandler.OnEndAttack += OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse += OnImpulse;
        
        _character.AnimationController.ModelEventsHandler.OnOpenComboWindow += OnOpenComboWindow;
        _character.AnimationController.ModelEventsHandler.OnCloseComboWindow += OnCloseComboWindow;
    }

    public void HandleAttack()
    {
        if (_character.CombatSystem.ComboSystem.CanContinue && !_isAttackHandle)
        {
            _isAttackHandle = true;
        }
    }
    
    private void Attack()
    {
        var attack = _character.CombatSystem.ComboSystem.GetCurrentAttack();
        
        if (attack == null)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
            return;
        }
        
        Debug.Log(attack.AnimationName);
        
        _character.AnimationController.Attack();
        _character.CombatSystem.ComboSystem.OnAttack();
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

    private void OnOpenComboWindow()
    {
        _character.CombatSystem.ComboSystem.AllowNext();
    }
    
    private void OnCloseComboWindow()
    {
        if (_isAttackHandle)
        {
            _character.CombatSystem.ComboSystem.NextStep();
            Attack();
            _isAttackHandle = false;
        }
    }

    public override void Update()
    {
        if (_isAttackFinished)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public bool TryNextAttack()
    {
        return _character.CombatSystem.ComboSystem.CanContinue;
    }

    public override void Exit()
    {
        _character.AnimationController.ModelEventsHandler.OnEndAttack -= OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse -= OnImpulse;
        
        _character.AnimationController.ModelEventsHandler.OnOpenComboWindow -= OnOpenComboWindow;
        _character.AnimationController.ModelEventsHandler.OnCloseComboWindow -= OnCloseComboWindow;
        
        _isAttackFinished = false;
        _isAttackHandle = false;
        
        _character.CombatSystem.ComboSystem.ResetCombo();
    }
}