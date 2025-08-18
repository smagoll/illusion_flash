public class CharacterBlockState : CharacterState
{
    public CharacterBlockState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
       
        _character.AnimationController.Block(true);
    }

    public override void Update()
    {
        if (!_character.CombatSystem.BlockSystem.IsBlocked)
        {
            _stateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public override void Exit()
    {
        _character.AnimationController.Block(false);
    }
}