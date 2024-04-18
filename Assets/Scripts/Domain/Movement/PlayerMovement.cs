using Pong.Domain.CameraHandling;
using Pong.Domain.Inputs;
using UnityEngine;

namespace Pong.Domain.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private CameraBoundsCalculator _cameraBoundsCalculator;
        private PlayerInput _input;

        public void Initialize(CameraBoundsCalculator cameraBoundsCalculator)
        {
            TryGetComponent(out _input);
            _cameraBoundsCalculator = cameraBoundsCalculator;
        }
    }
}
