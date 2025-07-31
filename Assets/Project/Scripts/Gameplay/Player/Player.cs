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
    
        public MovementController Movement => movementController;

        private void Update()
        {
            animationController.UpdateSpeed(movementController.HorizontalSpeed);
        }
    }
}


