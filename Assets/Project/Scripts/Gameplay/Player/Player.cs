using System;
using Input;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private MovementController movementController;
        [SerializeField]
        private AnimationController animationController;
        
        private IInputService _input;
    
        [Inject]
        public void Construct(IInputService input)
        {
            _input = input;
        }

        private void Update()
        {
            movementController.Move(_input.MoveAxis);
            animationController.UpdateSpeed(movementController.HorizontalSpeed);
            
            Debug.Log(movementController.HorizontalSpeed);
        }
    }
}


