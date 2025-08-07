using NodeCanvas.Framework;
using UnityEngine;

public class AttackNode : CharacterNodeBase
{
    protected override void OnExecute()
    {
        var bb = Character.Blackboard;
        var abilityController = Character.AbilityController;

        bool started = bb.GetVariable<bool>(BBKeys.AttackStarted).value;

        if (!started)
        {
            if (!abilityController.TryExecute(AbilityKeys.Attack))
            {
                EndAction(false);
                return;
            }

            bb.SetVariableValue(BBKeys.AttackStarted, true);
            return;
        }

        if (abilityController.IsPerformingAbility)
        {
            return;
        }

        bb.SetVariableValue(BBKeys.AttackStarted, false);
        EndAction(true);
    }
}