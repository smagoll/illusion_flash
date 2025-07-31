using Input;
using Zenject;

public class PlayerController : ITickable
{
    private readonly IInputService _input;
    private readonly Player.Player _player;

    public PlayerController(IInputService input, Player.Player player)
    {
        _input = input;
        _player = player;
    }

    public void Tick()
    {
        _player.Movement.Move(_input.MoveAxis);
    }
}