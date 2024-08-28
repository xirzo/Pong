using System;
using Pong.Domain.Score;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pong.Domain.Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallMovement : MonoBehaviour, ILoser
	{
		[SerializeField] [Min(0)] private float _speed = 100f;
		[SerializeField] [Min(0)] private float _maxSpeed = 350f;
		[SerializeField] [Min(0)] private float _maximumVelocity = 10f;
		[Space] [SerializeField] [Min(0)] private float _hitSpeedMultiplier = 1.25f;
		[Space] [SerializeField] private LayerMask _repulseLayer;
		private Vector2 _defaultVelocity;

		private Vector2 _direction;

		private Rigidbody2D _rigidbody;
		private Vector2 _startPosition;
		private Vector2 _velocity;

		// FIXME: MOVEMENT VELOCITY IS INCONSISTENT

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

		private void OnCollisionEnter2D(Collision2D other)
		{
			OnCollide?.Invoke();

			if (((1 << other.gameObject.layer) & _repulseLayer) == 0) return;

			CalculateReflectionAndSetDirection(other.contacts[0].normal);
		}

		private void OnDrawGizmos()
		{
			if (Application.isPlaying == false)
				return;

			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, _direction);
		}

		public void Reset()
		{
			transform.position = _startPosition;
			_velocity = Vector3.zero;
			SetRandomDirection();
		}

		private void SetRandomDirection()
		{
			var x = Random.Range(-1f, 1f);
			var y = Random.Range(-1f, 1f);
			_direction = new Vector2(x, y);
		}

		public event Action OnCollide;

		private void CalculateReflectionAndSetDirection(Vector2 normal)
		{
			_direction = Vector2.Reflect(_direction, normal);
		}

		private void Move()
		{
			_velocity = _direction * _speed;

			_rigidbody.velocity = Vector2.ClampMagnitude(_velocity, _maximumVelocity);
		}
	}
}