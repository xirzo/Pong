using Pong.Domain.Bounds;
using UnityEngine;
using Zenject;

namespace Pong.Installers
{
	public class BoundsInstaller : MonoInstaller
	{
		[SerializeField] private CameraBoundsCalculator _cameraBoundsCalculator;

		public override void InstallBindings()
		{
		}
	}
}