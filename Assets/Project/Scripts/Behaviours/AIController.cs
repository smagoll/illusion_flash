using System;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

public class AIController : ICharacterController
{
    private Character _character;
    private readonly BehaviourTree _behaviourTree;
    
    private BehaviourTreeOwner _behaviourTreeOwner;
    private Blackboard _blackboard;

    public void Init(Character character)
    {
        _character = character;

        _behaviourTreeOwner = character.GetComponent<BehaviourTreeOwner>();
        _blackboard = character.GetComponent<Blackboard>();

        if (_behaviourTreeOwner == null || _blackboard == null)
        {
            Debug.LogError("Missing BehaviourTreeOwner or Blackboard on character prefab!");
            return;
        }
        
        _behaviourTreeOwner.StartBehaviour();
    }

    public void Tick()
    {
        //_behaviourTree.Tick(_character);
    }
}