using System;

public interface IAbility
{
    string Id { get; }
    void Initialize(Character character);
    void Cleanup();
    void Execute();
    void HandleAlreadyInState();
    void OnUpdate();
    bool CanExecute();
    bool IsFinished { get; }
}