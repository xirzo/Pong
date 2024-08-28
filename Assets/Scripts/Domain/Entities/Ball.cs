using Pong.Domain.Movement;
using Pong.Domain.Score;
using UnityEngine;

namespace Pong.Domain.Entities
{
	public class Ball : MonoBehaviour, ILoser
	{
		private BallMovement _ballMovement;
		private Vector3 _startPosition;

		private void Awake()
		{
			_startPosition = transform.position;
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
	}
}