using UnityEngine;

public class AttackAbility : Ability
{
    private WeaponController _weaponController;
    
    private bool _isAttackHandled;
    private bool _isAttackFinished;

    private ModelEventsHandler _events;

    private float _stamina;

    public override bool IsFinished => _isAttackFinished;

    public AttackAbility(string id, float stamina) : base(id)
    {
        _stamina = stamina;
    }

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        _weaponController = character.WeaponController;
        _events = character.AnimationController.ModelEventsHandler;
    }

    public override bool CanExecute()
    {
        if (_isAttackFinished) return false;
        if (_weaponController?.IsWeaponDrawn != true) return false;

        var attack = Character.CombatSystem.ComboSystem.GetCurrentAttack();
        if (attack == null) return false;

        return Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        StartAttack();
        UseStamina();
    }

    private void StartAttack()
    {
        _isAttackHandled = false;
        _isAttackFinished = false;

        SubscribeEvents();
        PlayAttackAnimation();
        Character.CombatSystem.ComboSystem.OnAttack();
    }

    private void PlayAttackAnimation()
    {
        var attack = Character.CombatSystem.ComboSystem.GetCurrentAttack();
        if (attack == null) return;

        Debug.Log(attack.AnimationName);
        Character.AnimationController.Attack();
    }

    private void UseStamina()
    {
        Character.Model.UseStamina(_stamina);
    }

    public override void HandleAlreadyInState()
    {
        if (Character.CombatSystem.ComboSystem.CanContinue && !_isAttackHandled)
        {
            _isAttackHandled = true;
        }
    }

    public override void OnUpdate()
    {
        if (_isAttackFinished)
        {
            UnsubscribeEvents();
            Character.StateMachine.TrySetState<CharacterIdleState>();
        }
    }

    private void SubscribeEvents()
    {
        _events.OnEndAttack += OnAttackFinished;
        _events.OnImpulse += OnImpulse;
        _events.OnOpenComboWindow += OnOpenComboWindow;
        _events.OnCloseComboWindow += OnCloseComboWindow;
    }

    private void UnsubscribeEvents()
    {
        _events.OnEndAttack -= OnAttackFinished;
        _events.OnImpulse -= OnImpulse;
        _events.OnOpenComboWindow -= OnOpenComboWindow;
        _events.OnCloseComboWindow -= OnCloseComboWindow;
    }

    private void OnAttackFinished() => _isAttackFinished = true;

    private void OnImpulse()
    {
        Vector3 forward = Character.MovementController.Forward;
        Character.MovementController.ApplyImpulse(forward, 5f);
    }

    private void OnOpenComboWindow()
    {
        Character.CombatSystem.ComboSystem.AllowNext();
    }

    private void OnCloseComboWindow()
    {
        if (_isAttackHandled)
        {
            Character.CombatSystem.ComboSystem.NextStep();
            StartAttack();
            _isAttackHandled = false;
        }
    }

    public override void Cleanup()
    {
        UnsubscribeEvents();
        _isAttackHandled = false;
        _isAttackFinished = false;
        Character.CombatSystem.ComboSystem.ResetCombo();
    }
}
