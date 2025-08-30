using System.Collections;
using UnityEngine;

public class ClapHealAbility : Ability
{
    private int _healAmount;
    private float _cooldown;
    private bool _isOnCooldown;

    public override bool IsFinished => true;
    public override bool IsMove => true;

    public ClapHealAbility(string id, int healAmount, float staminaCost, float cooldown) : base(id)
    {
        _healAmount = healAmount;
        _cooldown = cooldown;
    }

    public override bool CanExecute()
    {
        bool canUse = !_isOnCooldown &&
                      (Character.StateMachine.IsState<CharacterIdleState>() || Character.StateMachine.IsState<CharacterLocomotionState>());
        return canUse;
    }

    public override void Execute()
    {
        if (!CanExecute())
            return;

        Character.Model.Health.Heal(_healAmount);
        

        Character.StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _isOnCooldown = false;
    }
}