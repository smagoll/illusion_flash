public class StunEffect : StatusEffectBase
{
    public StunEffect(float duration) : base("Stun", duration) { }

    public override void Apply(Character character)
    {
        character.StateMachine.SetState<CharacterStunState>();
        var stunState = character.StateMachine.GetState<CharacterStunState>();
        stunState.SetDuration(Duration);
        character.StateMachine.SetState<CharacterStunState>();
    }
}