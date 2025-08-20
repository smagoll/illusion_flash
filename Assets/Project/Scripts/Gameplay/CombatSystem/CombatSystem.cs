using UnityEngine;

public class CombatSystem
{
    private Character _character;
    
    public ParrySystem ParrySystem { get; private set; }
    public ComboSystem ComboSystem { get; private set; }
    public BlockSystem BlockSystem { get; private set; }
    
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
        if (BlockSystem.IsBlocked)
        {
            if (ParrySystem.IsParryActive)
            {
                _character.AnimationController.ParrySuccess();
                Debug.Log("Парирование успешно!");
                return;
            }
            
            _character.AnimationController.BlockDamage();
            int reducedDamage = BlockSystem.ReduceDamage(damageData.Damage);
            _character.Model.TakeDamage(reducedDamage);
            return;
        }

        _character.Model.TakeDamage(damageData.Damage);
    }
}