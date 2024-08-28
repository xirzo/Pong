using Pong.Domain.Entities;
using Pong.Domain.Movement;
using UnityEngine;
using Zenject;

namespace Pong.Installers
{
	public class BallInstaller : MonoInstaller
	{
		[SerializeField] private Ball _ball;
		[SerializeField] private Rigidbody2D _rigidbody;

		[Space] [SerializeField] private float _speed = 100f;

		[SerializeField] private float _maximumVelocity = 10f;
		private BallMovement _ballMovement;

		public override void InstallBindings()
		{
			_ballMovement = new BallMovement(_speed, _maximumVelocity, _rigidbody);
			Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();
			Container.BindInterfacesAndSelfTo<BallMovement>().FromInstance(_ballMovement).AsSingle();
			_ball.Construct(_ballMovement);
			Container.Bind<Ball>().FromInstance(_ball).AsSingle();
		}
	}
}