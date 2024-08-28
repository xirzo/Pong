using Pong.Domain.Movement;
using UnityEngine;

namespace Pong.Domain.Score
{
	public class LoseVolume : MonoBehaviour
	{
		private ScoreCounter _scoreCounter;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out BallMovement ballMovement) == false) return;

			_scoreCounter.Increase();
		}

		public void Construct(ScoreCounter scoreCounter)
		{
			_scoreCounter = scoreCounter;
		}
	}
}