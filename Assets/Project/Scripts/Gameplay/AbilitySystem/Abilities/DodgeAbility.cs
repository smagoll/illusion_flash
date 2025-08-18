public class DodgeAbility : Ability
{
    private float _stamina;
    
    public override bool IsFinished => true;

    public DodgeAbility(string id, float stamina) : base(id)
    {
        _stamina = stamina;
    }

    public override bool CanExecute()
    {
        return (Character.StateMachine.IsState<CharacterLocomotionState>() || Character.StateMachine.IsState<CharacterIdleState>()) && Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        var success = Character.StateMachine.TrySetState<CharacterDodgeState>();
        if(success)Character.Model.UseStamina(_stamina);
    }
}