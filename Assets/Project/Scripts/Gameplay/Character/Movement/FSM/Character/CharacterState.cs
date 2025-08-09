public abstract class CharacterState
{
    protected readonly CharacterStateMachine _stateMachine;
    protected readonly Character _character;

    public CharacterState(CharacterStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _character = stateMachine.Character;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}