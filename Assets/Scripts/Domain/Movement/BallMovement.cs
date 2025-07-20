using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Pong.Domain.Movement
{
	public class BallMovement : IInitializable, IFixedTickable
	{
		private readonly float _speed = 100f;
		private readonly float _maximumVelocity = 10f;

		private const float MinAngle = -35f;
		private const float MaxAngle = 30f;

		private readonly Rigidbody2D _rigidbody;

		private Vector2 _direction;
		private Vector2 _velocity;

		public BallMovement(float speed, float maximumVelocity,
			Rigidbody2D rigidbody)
		{
			_speed = speed;
			_maximumVelocity = maximumVelocity;
			_rigidbody = rigidbody;
		}

		public void FixedTick()
		{
			Move();
		}

		public void Initialize()
		{
			SetRandomDirection();
		}

		public void ResetVelocity()
		{
			_velocity = Vector3.zero;
		}

		public void SetRandomDirection()
		{
			var randomAngle = Random.Range(MinAngle, MaxAngle);
			var angleRad =  randomAngle * Mathf.Deg2Rad;
			var direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

			direction.x *= Random.value;

			_direction = direction.normalized;
		}

		public void CalculateReflectionAndSetDirection(Vector2 normal)
		{
			_direction = Vector2.Reflect(_direction, normal);
		}

		private void Move()
		{
			_velocity = _direction * _speed;

			_rigidbody.linearVelocity = Vector2.ClampMagnitude(_velocity, _maximumVelocity);
		}
	}
}