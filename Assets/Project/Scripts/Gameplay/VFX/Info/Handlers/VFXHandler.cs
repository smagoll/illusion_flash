using System;

public class VFXHandler : IDisposable
{
    public CombatVFXHandler CombatVFX { get; private set; }

    public VFXHandler(Character character)
    {
        CombatVFX = new CombatVFXHandler(character);
    }

    public void Dispose()
    {
        CombatVFX?.Dispose();
        CombatVFX = null;
    }
}