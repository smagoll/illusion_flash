using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    
    private ICharacterController _controller;

    public MovementController Movement => movementController;

    public void SetController(ICharacterController controller)
    {
        _controller = controller;
        _controller.Init(this);
    }
    
    private void Update()
    {
        animationController.UpdateSpeed(movementController.HorizontalSpeed);
    }
}