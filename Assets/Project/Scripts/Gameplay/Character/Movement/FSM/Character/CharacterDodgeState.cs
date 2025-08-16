using UnityEngine;

public class CharacterDodgeState : CharacterState
{
    private bool _isDodgeFinished;
    
    public CharacterDodgeState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        _character.MovementController.EnableDisableDetectCollisions(false);
        _character.AnimationController.Dodge();
        _isDodgeFinished = false;

        _character.AnimationController.ModelEventsHandler.OnEndDodge += OnDodgeFinished;
        _character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot += OnAnimationMoveRoot;
        
        Vector3 moveDir = _character.MovementController.LastMoveDirection;
        if (moveDir.sqrMagnitude > 0.001f)
        {
            if (moveDir != Vector3.zero)
            {
                _character.transform.rotation = Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.y));
            }
        }
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
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public override void Exit()
    {
        _character.MovementController.EnableDisableDetectCollisions(true);
        _character.AnimationController.ModelEventsHandler.OnEndAttack -= OnDodgeFinished;
        _character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot -= OnAnimationMoveRoot;
        
        Debug.Log("exit dodge state");
    }
}