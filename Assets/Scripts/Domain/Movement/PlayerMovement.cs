using Pong.Domain.Bounds;
using Pong.Domain.Inputs;
using UnityEngine;
using Zenject;

namespace Pong.Domain.Movement
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerMovement : MonoBehaviour
	{
		private const float SCALE_MULTIPLIER = 0.5f;
		[SerializeField] [Range(0, 1)] private float _speedMultiplier = 0.5f;
		private float _boundsY;
		private CameraBoundsCalculator _cameraBoundsCalculator;
		private PlayerInput _input;
		private float _movementInputY;

		private void Awake()
		{
			TryGetComponent(out _input);
		}

		private void Start()
		{
			_boundsY = _cameraBoundsCalculator.GetTopY();
		}

		private void Update()
		{
			UpdateInput();
			Move();
		}

		[Inject]
		private void Construct(CameraBoundsCalculator cameraBoundsCalculator)
		{
			_cameraBoundsCalculator = cameraBoundsCalculator;
		}

		private void UpdateInput()
		{
			_movementInputY = _input.Actions.Player.Movement.ReadValue<float>();
		}

		private void Move()
		{
			var positionOffset = transform.localScale.y * new Vector3(0, _movementInputY / 2) * _speedMultiplier;

			transform.position += positionOffset;
			transform.position = new Vector3(transform.position.x,
				Mathf.Clamp(transform.position.y, -(_boundsY - transform.localScale.y * SCALE_MULTIPLIER),
					_boundsY - transform.localScale.y * SCALE_MULTIPLIER), transform.position.z);
		}
	}
}