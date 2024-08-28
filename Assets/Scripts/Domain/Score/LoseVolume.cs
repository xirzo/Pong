using UnityEngine;

namespace Pong.Domain.Score
{
	public class LoseVolume : MonoBehaviour
	{
		private ScoreCounter _scoreCounter;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out ILoser loser) == false) return;

			loser.Reset();
			_scoreCounter.Increase();
		}

		public void Construct(ScoreCounter scoreCounter)
		{
			_scoreCounter = scoreCounter;
		}
	}
}