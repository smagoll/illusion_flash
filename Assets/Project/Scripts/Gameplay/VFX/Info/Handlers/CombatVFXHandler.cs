using System;
using UnityEngine;

public class CombatVFXHandler : IDisposable
{
    private Character _character;

    public CombatVFXHandler(Character character)
    {
        _character = character;

        _character.CombatSystem.OnCombatEvent += HandleCombatEvent;
    }
    
    private void HandleCombatEvent(HitInfo info)
    {
        switch(info.Type)
        {
            case HitType.Hit:
                HandleHitVFX(info);
                break;
            case HitType.Block:
                HandleBlockVFX(info);
                break;
            case HitType.Parry:
                HandleParryVFX(info);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleParryVFX(HitInfo info)
    {
        
    }

    private void HandleBlockVFX(HitInfo info)
    {
        
    }

    private void HandleHitVFX(HitInfo info)
    {
        VFXSystem.Instance.SpawnImpact(
            VFXSystem.Instance.library.swordImpact,
            info.HitPoint,
            info.HitNormal
        );
    }

    public void Dispose()
    {
        if (_character != null)
        {
            _character.CombatSystem.OnCombatEvent -= HandleCombatEvent;
        }

        _character = null;
    }
}