using Pong.Domain.Scenes;
using Pong.View.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuUI>().AsSingle().WithArguments(playButton, exitButton);
        }
    }
}
