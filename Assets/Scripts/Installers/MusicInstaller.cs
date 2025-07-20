using Pong.Domain.Music;
using Pong.View.Sound;
using UnityEngine;
using Zenject;

namespace Pong.Installers
{
    public class MusicInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip song;
        public override void InstallBindings()
        {
            Container.BindInstance(audioSource);
            Container.Bind<MusicPlayer>().AsSingle().WithArguments(song);
            Container.BindInterfacesAndSelfTo<MusicStarter>().AsSingle();
        }
    }
}