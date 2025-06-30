using Input;
using UnityEngine;
using Zenject;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    
    
    public void Move(Vector3 direction)
    {
        characterController.Move(direction);
    }
}
