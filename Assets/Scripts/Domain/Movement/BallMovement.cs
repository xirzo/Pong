using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Pong.Domain.Movement
{
	public class BallMovement : IInitializable, IFixedTickable
	{
		private readonly float _speed = 100f;
		private readonly float _maximumVelocity = 10f;

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
			var x = Random.Range(-1f, 1f);
			var y = Random.Range(-1f, 1f);
			_direction = new Vector2(x, y).normalized;
		}

		// private void OnCollisionEnter2D(Collision2D other)
		// {
		// 	OnCollide?.Invoke();
		//
		// 	if (((1 << other.gameObject.layer) & _repulseLayer) == 0) return;
		//
		// 	CalculateReflectionAndSetDirection(other.contacts[0].normal);
		// }
		//
		// public event Action OnCollide;

		// private void CalculateReflectionAndSetDirection(Vector2 normal)
		// {
		// 	_direction = Vector2.Reflect(_direction, normal);
		// }

		private void Move()
		{
			_velocity = _direction * _speed;

			_rigidbody.velocity = Vector2.ClampMagnitude(_velocity, _maximumVelocity);
		}
	}
}