using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private CharacterView characterView;
    
    private ICharacterController _controller;

    public MovementController Movement => movementController;
    
    public void SetController(ICharacterController controller)
    {
        _controller = controller;
        _controller.Init(this);

        var entity = new CharacterModel(100, 100);
        characterView.Init(entity);
        
        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
        
        animationController.UpdateSpeed(movementController.HorizontalSpeed);
    }
}