public interface IStatusEffect
{
    string Name { get; }
    float Duration { get; }
    bool IsExpired { get; }
    
    void Apply(Character character);
    void Remove(Character character);
    void Tick(Character character, float deltaTime);
}