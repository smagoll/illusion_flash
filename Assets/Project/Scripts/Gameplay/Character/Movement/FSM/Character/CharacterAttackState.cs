using UnityEngine;

public class CharacterAttackState : CharacterState
{
    private bool _isAttackFinished;

    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _character.MovementController.StopMove();
        
        _character.AnimationController.Attack();
        _isAttackFinished = false;

        _character.AnimationController.ModelEventsHandler.OnEndAttack += OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse += OnImpulse;
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
        if (_isAttackFinished)
        {
            _stateMachine.SetState<CharacterIdleState>();
        }
    }

    public override void Exit()
    {
        _character.AnimationController.ModelEventsHandler.OnEndAttack -= OnAttackFinished;
        _character.AnimationController.ModelEventsHandler.OnImpulse -= OnImpulse;
        
        _character.MovementController.ResumeMove();
    }
}