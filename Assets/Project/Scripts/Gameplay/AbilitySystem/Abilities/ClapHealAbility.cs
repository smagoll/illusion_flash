using System.Collections;
using UnityEngine;

public class ClapHealAbility : Ability
{
    private bool isFinished;
    private int _healAmount;

    public override bool IsFinished => isFinished;
    public override bool IsMove => true;

    public ClapHealAbility(string id, int healAmount) : base(id)
    {
        _healAmount = healAmount;
    }

    public override bool CanExecute()
    {
        bool canUse =
            Character.StateMachine.IsState<CharacterIdleState>() ||
            Character.StateMachine.IsState<CharacterLocomotionState>();
        
        return canUse;
    }

    public override void Execute()
    {
        Character.AnimationController.Clap();

        Character.AnimationController.ModelEventsHandler.OnEndAbility += Finish;
    }

    public override void Cleanup()
    {
        Character.AnimationController.ModelEventsHandler.OnEndAbility -= Finish;
    }

    private void Finish()
    {
        Character.Model.Health.Heal(_healAmount);
        isFinished = true;
    }
}