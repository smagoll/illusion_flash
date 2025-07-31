using Input;

public class PlayerController : ICharacterController
{
    private readonly IInputService _input;
    private Character character;

    public PlayerController(IInputService input)
    {
        _input = input;
    }

    public void Init(Character character)
    {
        this.character = character;
    }

    public void Tick()
    {
        character.Movement.Move(_input.MoveAxis);
    }
}