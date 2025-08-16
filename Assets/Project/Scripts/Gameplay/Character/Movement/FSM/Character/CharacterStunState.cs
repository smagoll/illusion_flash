using UnityEngine;

public class CharacterStunState : CharacterState
{
    private float _duration;
    private float _timer;
    
    public CharacterStunState(CharacterStateMachine stateMachine) : base(stateMachine) { }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }
    
    public override void Enter()
    {
        _timer = _duration;
        _character.AnimationController.Stun(true);
    }
    
    public override void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _stateMachine.ForceSetState<CharacterIdleState>();
        }
    }

    public override bool CanBeInterruptedBy(CharacterState newState)
    {
        return newState is CharacterDeathState;
    }

    public override void Exit()
    {
        _character.AnimationController.Stun(false);
    }
}