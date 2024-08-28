using UnityEngine;
using Zenject;

namespace Pong.Domain.Bounds
{
	[RequireComponent(typeof(Camera))]
	// TODO: Move to separate layer
	public sealed class CameraBoundsCalculator
	{
		private readonly Camera _camera;

		[Inject]
		public CameraBoundsCalculator(Camera camera)
		{
			_camera = camera;
		}

		public float GetTopY()
		{
			return GetPoint(0, 1).y;
		}

		public float GetBottomY()
		{
			return GetPoint(0, 0).y;
		}

		public Vector2[] GetBoundaryPoints()
		{
			return new Vector2[4]
			{
				GetPoint(0, 0),
				GetPoint(1, 0),
				GetPoint(0, 1),
				GetPoint(1, 1)
			};
		}

		public Vector2[] GetVerticalLineBoundaryPoints(float x)
		{
			return new Vector2[2]
			{
				GetPoint(x, 0),
				GetPoint(x, 1)
			};
		}

		public Vector2[] GetHorizontalLineBoundaryPoints(float y)
		{
			return new Vector2[2]
			{
				GetPoint(0, y),
				GetPoint(1, y)
			};
		}


		private Vector2 GetPoint(float x, float y)
		{
			return new Vector2(_camera.ViewportToWorldPoint(new Vector3(x, y)).x,
				_camera.ViewportToWorldPoint(new Vector3(x, y)).y);
		}
	}
}