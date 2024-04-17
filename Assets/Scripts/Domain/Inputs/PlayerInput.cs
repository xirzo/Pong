using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.Domain.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        public InputActions Actions { get; private set; }

        private void Awake()
        {
            Actions = new InputActions();
        }

        private void OnEnable()
        {
            Actions.Enable();
        }

        private void OnDisable()
        {
            Actions.Disable();
        }
    }
}
