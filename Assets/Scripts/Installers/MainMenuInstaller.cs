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
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [Space]
        [SerializeField] private Button settingsBackButton;
        [SerializeField] private Scrollbar scrollbar;
        [Space]
        [SerializeField] private StringMenuDictionary menus;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuSelector>().AsSingle().WithArguments(menus);
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuStarter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuUI>().AsSingle().WithArguments(playButton, settingsButton, exitButton);
            Container.BindInterfacesAndSelfTo<SettingsMenuUI>().AsSingle().WithArguments(settingsBackButton, scrollbar);
        }
    }
}
