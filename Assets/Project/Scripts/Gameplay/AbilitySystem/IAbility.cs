public interface IAbility
{
    string Id { get; }
    void Initialize(Character character);
    void Cleanup();
    public void Execute();
    bool CanExecute();
    bool IsFinished { get; }
}