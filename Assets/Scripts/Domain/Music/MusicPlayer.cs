using UnityEngine;
using Zenject;

namespace Pong.Domain.Music
{
    public class MusicPlayer
    {
        private readonly AudioSource _audioSource;
        private readonly AudioClip _clip;

        [Inject]
        public MusicPlayer(AudioSource audioSource, AudioClip clip)
        {
            _audioSource = audioSource;
            _clip = clip;
        }

        public void Play()
        {
            _audioSource.loop = true;
            _audioSource.clip = _clip;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Pause();
        }
    }
}
