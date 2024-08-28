using System;
using Zenject;

namespace Pong.Domain.Inputs
{
	public sealed class PlayerInput : IInitializable, IDisposable
	{
		public InputActions Actions { get; private set; }

		public void Dispose()
		{
			Actions.Dispose();
		}

		public void Initialize()
		{
			Actions = new InputActions();
			Actions.Enable();
		}
	}
}