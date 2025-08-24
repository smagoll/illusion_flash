using UnityEngine;

public class DodgeAbility : Ability
{
    private float _stamina;
    private bool _isDodgeFinished;

    public override bool IsFinished => _isDodgeFinished;

    public DodgeAbility(string id, float stamina) : base(id)
    {
        _stamina = stamina;
    }

    public override bool CanExecute()
    {
        return (Character.StateMachine.IsState<CharacterLocomotionState>() ||
                Character.StateMachine.IsState<CharacterIdleState>()) &&
               Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        UseStamina();
        StartDodge();
    }

    private void UseStamina()
    {
        Character.Model.UseStamina(_stamina);
    }

    private void StartDodge()
    {
        _isDodgeFinished = false;

        Character.MovementController.EnableDisableDetectCollisions(false);

        Character.AnimationController.Dodge();

        Character.AnimationController.ModelEventsHandler.OnEndDodge += OnDodgeFinished;
        Character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot += OnAnimationMoveRoot;

        Vector3 moveDir = Character.MovementController.LastMoveDirection;
        if (moveDir.sqrMagnitude > 0.001f)
        {
            Character.transform.rotation = Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.y));
        }
    }

    private void OnDodgeFinished()
    {
        _isDodgeFinished = true;
        Cleanup();
    }

    private void OnAnimationMoveRoot(Vector3 position, Quaternion rotation)
    {
        Character.transform.position += position * 2;
    }

    public override void OnUpdate()
    {
        if (_isDodgeFinished)
        {
            Character.StateMachine.TrySetState<CharacterIdleState>();
        }
    }

    public override void Cleanup()
    {
        Character.MovementController.EnableDisableDetectCollisions(true);
        Character.AnimationController.ModelEventsHandler.OnEndDodge -= OnDodgeFinished;
        Character.AnimationController.ModelEventsHandler.OnAnimatorMoveRoot -= OnAnimationMoveRoot;
    }
}
