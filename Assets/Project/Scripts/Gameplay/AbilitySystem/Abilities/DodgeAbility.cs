public class DodgeAbility : Ability
{
    private WeaponController _weaponController;

    private float _stamina;
    private bool _isAttacking;
    
    public override bool IsFinished => !_isAttacking;

    public DodgeAbility(string id, float stamina) : base(id)
    {
        _stamina = stamina;
    }
    
    public override void Initialize(Character character)
    {
        base.Initialize(character);
        
        _weaponController = character.WeaponController;
    }

    public override bool CanExecute()
    {
        return (Character.StateMachine.IsState<CharacterLocomotionState>() || Character.StateMachine.IsState<CharacterIdleState>()) && Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        Character.StateMachine.SetState<CharacterDodgeState>();
        
        Character.Model.UseStamina(_stamina);
    }
}