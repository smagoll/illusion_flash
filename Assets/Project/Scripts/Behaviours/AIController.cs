using System;
using NodeCanvas.BehaviourTrees;
using Object = UnityEngine.Object;

public class AIController : ICharacterController
{
    private Character _character;
    private readonly BehaviourTree _behaviourTree;
    
    public BehaviourTree BehaviourTree => _behaviourTree;

    public AIController(BehaviourTree behaviourTree)
    {
        _behaviourTree = behaviourTree;
    }

    public void Init(Character character)
    {
        _character = character;

        character.BehaviourTreeOwner.StartBehaviour();
    }

    public void Tick()
    {
        //_behaviourTree.Tick(_character);
    }
}