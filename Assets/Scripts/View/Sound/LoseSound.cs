using System;
using Pong.Domain.Entities;
using Pong.Domain.Score;
using UnityEngine;

namespace Pong.View.Sound
{
	public class LoseSound : IDisposable
	{
		private readonly Ball _ball;
		private readonly AudioSource _audioSource;
		private readonly AudioClip _clip;
		private readonly LoseVolume _loseVolume;

		public LoseSound(AudioSource audioSource, AudioClip clip, LoseVolume loseVolume)
		{
			_audioSource = audioSource;
			_clip = clip;
			_loseVolume = loseVolume;

			_loseVolume.OnLose += PlaySound;
		}

		public void Dispose()
		{
			_loseVolume.OnLose -= PlaySound;
		}

		private void PlaySound()
		{
			_audioSource.PlayOneShot(_clip);
		}
	}
}