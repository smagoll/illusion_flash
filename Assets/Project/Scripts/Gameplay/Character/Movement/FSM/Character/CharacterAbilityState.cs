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
    }

    public override void Update()
    {
        _currentAbility?.OnUpdate();

        if (_currentAbility?.IsFinished == true)
            _stateMachine.TrySetState<CharacterIdleState>();
    }

    public override void Exit()
    {
        _currentAbility?.Cleanup();
        _currentAbility = null;
    }
}