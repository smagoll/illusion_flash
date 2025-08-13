public class JumpAbility : Ability
{
    private float _jumpForce;

    private float _stamina;
    
    public override bool IsFinished => true;
    
    public JumpAbility(string id, float jumpForce, float stamina) : base(id)
    {
        _jumpForce = jumpForce;
        _stamina = stamina;
    }

    public override bool CanExecute()
    {
        return Character.MovementController.IsGrounded && Character.Model.Stamina.Current >= _stamina;
    }

    public override void Execute()
    {
        Character.Model.UseStamina(_stamina);
        
        Character.MovementController.ApplyVerticalVelocity(_jumpForce);
        Character.AnimationController.Jump();
    }
}