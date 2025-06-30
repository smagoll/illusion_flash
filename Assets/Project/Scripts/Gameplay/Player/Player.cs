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
        
        private IInputService _input;
    
        [Inject]
        public void Construct(IInputService input)
        {
            _input = input;
        }

        private void Update()
        {
            movementController.Move(_input.MoveAxis);
        }
    }
}


