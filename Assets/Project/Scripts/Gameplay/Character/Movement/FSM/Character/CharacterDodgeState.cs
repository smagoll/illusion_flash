using UnityEngine;

public class CharacterDodgeState : CharacterState
{
    private bool _isDodgeFinished;
    
    public CharacterDodgeState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        _character.MovementController.Collider.enabled = false;
        _character.MovementController.StopMove();
        _character.AnimationController.Dodge();
        _isDodgeFinished = false;

        _character.AnimationController.ModelEventsHandler.OnEndDodge += OnDodgeFinished;
        _character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot += OnAnimationMoveRoot;
    }

    private void OnDodgeFinished()
    {
        _isDodgeFinished = true;
    }

    private void OnAnimationMoveRoot(Vector3 position, Quaternion rotation)
    {
        _character.transform.position += position;
        _character.transform.rotation *= rotation;
    }

    public override void Update()
    {
        if (_isDodgeFinished)
        {
            _stateMachine.SetState<CharacterIdleState>();
        }
    }

    public override void Exit()
    {
        _character.MovementController.Collider.enabled = true;
        _character.AnimationController.ModelEventsHandler.OnEndAttack -= OnDodgeFinished;
        _character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot -= OnAnimationMoveRoot;
        
        _character.MovementController.ResumeMove();
    }
}