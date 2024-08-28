using Pong.Domain.Bounds;
using Pong.Domain.Movement;
using Pong.Domain.Score;
using Pong.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Pong.Installers
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private LoseVolume _loseVolume;

		[Space] [SerializeField] private Transform _player;

		[SerializeField] private TextMeshProUGUI _textfield;
		private ScoreCounter _scoreCounter;

		public override void InstallBindings()
		{
			// REMOVE SCORE INIT
			_scoreCounter = new ScoreCounter();
			Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
			_loseVolume.Construct(_scoreCounter);
			Container.Bind<TextMeshProUGUI>().FromInstance(_textfield);
			Container.BindInterfacesAndSelfTo<ScoreCounterUI>().AsSingle();
			Container.Bind<CameraBoundsCalculator>().AsSingle();
			Container.Bind<Transform>().FromInstance(_player).AsSingle();
			Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
		}
	}
}