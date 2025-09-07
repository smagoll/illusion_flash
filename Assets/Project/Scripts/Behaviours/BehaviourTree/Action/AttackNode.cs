using NodeCanvas.Framework;
using UnityEngine;

public class AttackAction : CharacterActionBase
{
    private AbilityController abilityController;

    protected override void OnExecute()
    {
        abilityController = Character.AbilityController;
        EndAction(abilityController.TryExecute(AbilityKeys.Attack));
    }
}