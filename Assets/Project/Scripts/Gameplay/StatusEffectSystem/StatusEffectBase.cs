public abstract class StatusEffectBase : IStatusEffect
{
    public string Name { get; private set; }
    public float Duration { get; private set; }
    private float _elapsedTime;

    public bool IsExpired => _elapsedTime >= Duration;

    protected StatusEffectBase(string name, float duration)
    {
        Name = name;
        Duration = duration;
        _elapsedTime = 0f;
    }

    public virtual void Apply(Character character) { }
    public virtual void Remove(Character character) { }

    public void Tick(Character character, float deltaTime)
    {
        _elapsedTime += deltaTime;
        OnTick(character, deltaTime);

        if (IsExpired)
        {
            Remove(character);
        }
    }

    protected virtual void OnTick(Character character, float deltaTime) { }
}