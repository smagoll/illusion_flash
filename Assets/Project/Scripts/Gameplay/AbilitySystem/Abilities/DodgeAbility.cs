public class DodgeAbility : Ability
{
    private WeaponController _weaponController;

    private bool _isAttacking;
    
    public override bool IsFinished => !_isAttacking;

    public DodgeAbility(string id) : base(id)
    {
        
    }
    
    public override void Initialize(Character character)
    {
        base.Initialize(character);
        
        _weaponController = character.WeaponController;
    }

    public override bool CanExecute()
    {
        return Character.StateMachine.IsState<CharacterLocomotionState>() || Character.StateMachine.IsState<CharacterIdleState>();
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        Character.StateMachine.SetState<CharacterDodgeState>();
        
        Character.Model.UseStamina(10);
    }
}