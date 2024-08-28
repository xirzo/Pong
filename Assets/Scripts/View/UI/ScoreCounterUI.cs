using System;
using Pong.Domain.Score;
using TMPro;
using Zenject;

namespace Pong.View.UI
{
	public class ScoreCounterUI : IDisposable
	{
		private readonly ScoreCounter _scoreCounter;
		private readonly TextMeshProUGUI _textfield;

		[Inject]
		public ScoreCounterUI(ScoreCounter scoreCounter, TextMeshProUGUI textfield)
		{
			_scoreCounter = scoreCounter;
			_textfield = textfield;
			_scoreCounter.OnScoreChanged += Render;
		}

		public void Dispose()
		{
			_scoreCounter.OnScoreChanged -= Render;
		}

		private void Render(int score)
		{
			_textfield.text = score.ToString();
		}
	}
}