using System;

public class CombatSystem
{
    private Character _character;

    public ParrySystem ParrySystem { get; private set; }
    public ComboSystem ComboSystem { get; private set; }
    public BlockSystem BlockSystem { get; private set; }

    public event Action<HitInfo> OnCombatEvent;

    public CombatSystem(Character character)
    {
        _character = character;

        ParrySystem = new ParrySystem(_character);
        ComboSystem = new ComboSystem();
        BlockSystem = new BlockSystem();
    }

    public void ActivateBlock(bool isActive)
    {
        BlockSystem.Block(isActive);

        if (isActive)
        {
            ParrySystem.TryParry();
        }
    }

    public void HandleIncomingDamage(DamageData damageData)
    {
        var hitInfo = new HitInfo
        {
            HitPoint = damageData.HitPoint,
            HitNormal = -_character.transform.forward,
            Attacker = damageData.Attacker,
            Defender = _character,
            Type = HitType.Hit
        };

        if (BlockSystem.IsBlocked)
        {
            if (ParrySystem.IsParryActive)
            {
                _character.AnimationController.ParrySuccess();
                hitInfo.Type = HitType.Parry;
                OnCombatEvent?.Invoke(hitInfo);
                return;
            }

            _character.AnimationController.BlockDamage();
            int reducedDamage = BlockSystem.ReduceDamage(damageData.Damage);
            _character.Model.TakeDamage(reducedDamage);

            hitInfo.Type = HitType.Block;
            OnCombatEvent?.Invoke(hitInfo);
            return;
        }

        _character.Model.TakeDamage(damageData.Damage);

        OnCombatEvent?.Invoke(hitInfo);
    }
}