using System;
using Object = UnityEngine.Object;

public class AIController : ICharacterController, IDisposable
{
    private Character _character;
    private readonly BehaviourTree _behaviourTree;
    
    private BehaviourTreeAgent _behaviourTreeAgent;
    
    public BehaviourTree BehaviourTree => _behaviourTree;

    public AIController(BehaviourTree behaviourTree)
    {
        _behaviourTree = behaviourTree;
    }

    public void Init(Character character)
    {
        _character = character;

        _behaviourTreeAgent = character.gameObject.AddComponent<BehaviourTreeAgent>();
        _behaviourTreeAgent.Init(this);
    }

    public void Tick()
    {
        _behaviourTree.Tick(_character);
    }
    
    public void DebugDraw()
    {
        _behaviourTree.DrawGizmos();
    }

    public void Dispose()
    {
        Object.Destroy(_behaviourTreeAgent);
    }
}