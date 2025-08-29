public class CharacterAbilityState : CharacterState
{
    private IAbility _currentAbility;
    
    public CharacterAbilityState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public void UseAbility(IAbility ability)
    {
        _currentAbility = ability;
        _currentAbility.Execute();

        if (!_currentAbility.IsMove) _character.MovementController.StopMove();
    }

    public override void Update()
    {
        _currentAbility?.OnUpdate();

        if (_currentAbility?.IsFinished == true)
            _stateMachine.TrySetState<CharacterIdleState>();
    }

    public override void Exit()
    {
        if (!_currentAbility.IsMove) _character.MovementController.ResumeMove();
        _currentAbility?.Cleanup();
        _currentAbility = null;
    }
}