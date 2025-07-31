public class AIController : ICharacterController
{
    private Character _character;
    private Node _root;

    public AIController(Node rootNode)
    {
        _root = rootNode;
    }

    public void Init(Character character)
    {
        _character = character;
    }

    public void Tick()
    {
        _root.Tick(_character);
    }
}