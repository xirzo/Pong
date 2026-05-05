using Pong.Domain.Bounds;
using Pong.Domain.Inputs;
using UnityEngine;
using Zenject;

namespace Pong.Domain.Movement
{
    public class PlayerMovement : IFixedTickable
    {
        private const float ScaleMultiplier = 0.5f;

        private readonly float _boundsY;
        private readonly float _speedMultiplier = 0.4f;
        private readonly Transform _transform;
        private readonly PlayerInput _input;
        private readonly PlayerSide _playerSide;
        private float _movementInputY;

        [Inject]
        private PlayerMovement(CameraBoundsCalculator cameraBoundsCalculator, PlayerInput input, Transform transform,
            PlayerSide playerSide)
        {
            _input = input;
            _transform = transform;
            _playerSide = playerSide;
            _boundsY = cameraBoundsCalculator.GetTopY();
        }

        public void FixedTick()
        {
            UpdateInput();
            Move();
        }

        private void UpdateInput()
        {
            _movementInputY = _playerSide == PlayerSide.Left
                ? _input.Actions.Players.LeftMovement.ReadValue<float>()
                : _input.Actions.Players.RightMovement.ReadValue<float>();
        }

        private void Move()
        {
            var positionOffset = new Vector3(0, _movementInputY / 2) * (_transform.localScale.y * _speedMultiplier);

            _transform.position += positionOffset;
            _transform.position = new Vector3(_transform.position.x,
                Mathf.Clamp(_transform.position.y, -(_boundsY - _transform.localScale.y * ScaleMultiplier),
                    _boundsY - _transform.localScale.y * ScaleMultiplier), _transform.position.z);
        }
    }
}