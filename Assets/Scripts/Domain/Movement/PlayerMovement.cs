using Pong.Domain.Bounds;
using Pong.Domain.Inputs;
using UnityEngine;
using Zenject;

namespace Pong.Domain.Movement
{
	public class PlayerMovement : IFixedTickable
	{
		private const float SCALE_MULTIPLIER = 0.5f;
		private readonly float _boundsY;
		private readonly float _speedMultiplier = 0.4f;
		private readonly Transform _transform;
		private readonly CameraBoundsCalculator _cameraBoundsCalculator;
		private readonly PlayerInput _input;
		private readonly PlayerSide _playerSide;
		private float _movementInputY;

		[Inject]
		private PlayerMovement(CameraBoundsCalculator cameraBoundsCalculator, PlayerInput input, Transform transform, PlayerSide playerSide)
		{
			_cameraBoundsCalculator = cameraBoundsCalculator;
			_input = input;
			_transform = transform;
			_playerSide = playerSide;
			_boundsY = _cameraBoundsCalculator.GetTopY();
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
				Mathf.Clamp(_transform.position.y, -(_boundsY - _transform.localScale.y * SCALE_MULTIPLIER),
					_boundsY - _transform.localScale.y * SCALE_MULTIPLIER), _transform.position.z);
		}
	}
}