using Zenject;

namespace Pong.Domain.Music
{
    public class MusicStarter : IInitializable
    {
        private readonly MusicPlayer _musicPlayer;

        public MusicStarter(MusicPlayer musicPlayer)
        {
            _musicPlayer = musicPlayer;
        }

        public void Initialize()
        {
            _musicPlayer.Play();
        }
    }
}