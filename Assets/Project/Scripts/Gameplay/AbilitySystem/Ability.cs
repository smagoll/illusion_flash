using System;

public abstract class Ability : IAbility
{
    public string Id { get; private set; }
    protected Character Character { get; private set; }
    
    public abstract bool IsFinished { get; }

    public Ability(string id)
    {
        Id = id;
    }
    
    public virtual void Initialize(Character character)
    {
        Character = character;
    }
    
    public virtual void Cleanup()
    {
    }
    
    public abstract void Execute();
    public virtual void HandleAlreadyInState()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual bool CanExecute()
    {
        return true;
    }
}