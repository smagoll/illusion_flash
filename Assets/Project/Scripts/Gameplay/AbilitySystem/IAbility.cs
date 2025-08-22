public interface IAbility
{
    string Id { get; }
    void Initialize(Character character);
    void Cleanup();
    public void Execute();
    void OnUpdate();
    bool CanExecute();
    bool IsFinished { get; }
}