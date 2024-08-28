using System;

namespace Pong.Domain.Score
{
	public sealed class ScoreCounter
	{
		private int _score;

		public void Reset()
		{
			_score = 0;
			OnScoreChanged?.Invoke(_score);
		}

		public void Increase()
		{
			_score++;
			OnScoreChanged?.Invoke(_score);
		}

		public event Action<int> OnScoreChanged;
	}
}