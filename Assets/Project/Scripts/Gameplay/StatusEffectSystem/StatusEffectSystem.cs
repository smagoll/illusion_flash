using System.Collections.Generic;

public class StatusEffectSystem
{
    private readonly Character _character;
    private readonly List<IStatusEffect> _activeEffects = new();

    public StatusEffectSystem(Character character)
    {
        _character = character;
    }

    public void AddEffect(IStatusEffect effect)
    {
        effect.Apply(_character);
        _activeEffects.Add(effect);
    }

    public void Tick(float deltaTime)
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            _activeEffects[i].Tick(_character, deltaTime);
            if (_activeEffects[i].IsExpired)
            {
                _activeEffects.RemoveAt(i);
            }
        }
    }

    public bool HasEffect<T>() where T : IStatusEffect
    {
        foreach (var effect in _activeEffects)
            if (effect is T) return true;
        return false;
    }
}