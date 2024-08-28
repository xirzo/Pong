using Pong.Domain.Inputs;
using Zenject;

namespace Pong.Installers
{
	// TODO: REMOVE THIS AND GIVE EACH PLAYER ITS OWN INPUT
	public class TestInputInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
		}
	}
}