using Pong.Domain.Inputs;
using UnityEngine;

namespace Pong.Domain.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _input;

        private void Awake()
        {
            TryGetComponent(out _input);
        }
    }
}
