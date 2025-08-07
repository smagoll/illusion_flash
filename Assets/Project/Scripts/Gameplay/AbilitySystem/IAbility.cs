public interface IAbility
{
    string Id { get; }
    void Initialize(Character character);
    void Cleanup(Character character);
    public void Execute();
    bool CanExecute();
    bool IsFinished { get; }
}