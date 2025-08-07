public class JumpAbility : Ability
{
    private float _jumpForce;
    
    public override bool IsFinished => true;
    
    public JumpAbility(string id, float jumpForce) : base(id)
    {
        _jumpForce = jumpForce;
    }

    public override bool CanExecute()
    {
        return Character.MovementController.IsGrounded;
    }

    public override void Execute()
    {
        Character.MovementController.ApplyVerticalVelocity(_jumpForce);
        Character.AnimationController.Jump();
    }
}