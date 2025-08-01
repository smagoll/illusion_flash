using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private CharacterView characterView;

    [Inject] private Blackboard globalBackboard;
    
    private ICharacterController _controller;

    public MovementController Movement => movementController;
    public LocalBlackboard Blackboard { get; private set; }
    
    public void SetController(ICharacterController controller)
    {
        Blackboard = new LocalBlackboard(globalBackboard);
        
        _controller = controller;
        _controller.Init(this);

        var model = new CharacterModel(100, 100);
        characterView.Init(model);
        
        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
    }
}