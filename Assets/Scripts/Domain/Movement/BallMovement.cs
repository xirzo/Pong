using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pong.Domain.Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallMovement : MonoBehaviour
	{
		[SerializeField] [Min(0)] private float _speed = 1f;
		[SerializeField] [Min(0)] private float _maximumVelocity = 10f;
		[Space] [SerializeField] [Min(0)] private float _hitSpeedMultiplier = 1f;

		private Vector2 _direction;

		private Rigidbody2D _rigidbody;
		private Vector2 _startPosition;
		private Vector2 _velocity;

		private void Awake()
		{
			TryGetComponent(out _rigidbody);
			_startPosition = transform.position;
			SetRandomDirection();
		}

		private void FixedUpdate()
		{
			Move();
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			OnCollide?.Invoke();

			// if (collision.gameObject.TryGetComponent(out Repulsor repulsor)) SetDirection(collision);
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying == false)
				return;

			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, _direction);
		}

		private void SetRandomDirection()
		{
			var x = Random.Range(-1f, 1f);
			var y = Random.Range(-1f, 1f);
			_direction = new Vector2(x, y);
		}

		public event Action OnCollide;

		public void SetDirection(Vector2 normal)
		{
			_direction = Vector2.Reflect(_direction, normal * _hitSpeedMultiplier);
		}

		public void SetDirection(Collision2D collision)
		{
			var normal = collision.contacts[0].normal;
			SetDirection(normal);
		}

		private void Move()
		{
			_velocity = _direction * (_speed * Time.deltaTime);

			_rigidbody.velocity = Vector2.ClampMagnitude(_velocity, _maximumVelocity);
		}
	}
}