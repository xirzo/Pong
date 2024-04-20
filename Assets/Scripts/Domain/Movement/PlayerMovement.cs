using System;
using Pong.Domain.CameraHandling;
using Pong.Domain.Inputs;
using UnityEngine;

namespace Pong.Domain.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float SCALE_MULTIPLIER = 1.5f;
        [SerializeField] private CameraBoundsCalculator _cameraBoundsCalculator;
        private PlayerInput _input;
        private float _movementInputY;
        private float _boundsY;

        private void Awake()
        {
            TryGetComponent(out _input);
        }

        private void Start()
        {
            _boundsY = _cameraBoundsCalculator.GetTopY();
        }

        private void UpdateInput()
        {
            _movementInputY = _input.Actions.Player.Movement.ReadValue<float>();
        }

        private void Move()
        {
            var nextPosition = transform.position + (transform.localScale.y * SCALE_MULTIPLIER) * new Vector3(0, _movementInputY / 2);

            if (Mathf.Abs(nextPosition.y) >= _boundsY)
            {
                return;
            }

            transform.position = nextPosition;
        }

        private void Update()
        {
            UpdateInput();
            Move();
        }
    }
}
