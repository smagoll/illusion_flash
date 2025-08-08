using NodeCanvas.Framework;
using UnityEngine;

public class AttackAction : CharacterActionBase
{
    public BBParameter<bool> attackStarted;

    private AbilityController abilityController;

    protected override void OnExecute()
    {
        abilityController = Character.AbilityController;
        
        if (!attackStarted.value)
        {
            if (!abilityController.TryExecute(AbilityKeys.Attack))
            {
                EndAction(false);
                return;
            }

            attackStarted.value = true;
        }
    }

    protected override void OnUpdate()
    {
        if (abilityController.IsPerformingAbility)
            return;
        
        attackStarted.value = false;
        EndAction(true);
    }
}