using Zenject;

public interface ICharacterController : ITickable
{
    void Init(Character character);
    new void Tick();
}