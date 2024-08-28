using System;
using Pong.Domain.Entities;
using UnityEngine;

namespace Pong.View.Sound
{
	public class BallHitSound : IDisposable
	{
		private readonly Ball _ball;
		private readonly AudioSource _audioSource;
		private readonly AudioClip _clip;

		public BallHitSound(AudioSource audioSource, Ball ball, AudioClip clip)
		{
			_audioSource = audioSource;
			_ball = ball;
			_clip = clip;

			_ball.OnCollide += PlaySound;
		}

		public void Dispose()
		{
			_ball.OnCollide -= PlaySound;
		}

		private void PlaySound()
		{
			_audioSource.PlayOneShot(_clip);
		}
	}
}