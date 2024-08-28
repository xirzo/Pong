using System;
using Pong.Domain.Movement;
using Pong.Domain.Score;
using UnityEngine;

namespace Pong.Domain.Entities
{
	public class Ball : MonoBehaviour, ILoser
	{
		[SerializeField] private LayerMask _repulseLayer;
		private BallMovement _ballMovement;
		private Vector3 _startPosition;

		private void Awake()
		{
			_startPosition = transform.position;
		}


		private void OnCollisionEnter2D(Collision2D other)
		{
			OnCollide?.Invoke();

			if (((1 << other.gameObject.layer) & _repulseLayer) == 0) return;

			_ballMovement.CalculateReflectionAndSetDirection(other.contacts[0].normal);
		}

		public void Reset()
		{
			transform.position = _startPosition;
			_ballMovement.ResetVelocity();
			_ballMovement.SetRandomDirection();
		}

		public void Construct(BallMovement ballMovement)
		{
			_ballMovement = ballMovement;
		}

		public event Action OnCollide;
	}
}