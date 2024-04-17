using UnityEngine;

namespace Pong.Domain.CameraHandling
{
    [RequireComponent(typeof(Camera))]
    public class CameraBoundsCalculator : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            TryGetComponent(out _camera);
        }

        private Vector2 GetCameraBoundaryPoint(float x, float y)
        {
            return new Vector2(_camera.ViewportToWorldPoint(new Vector3(x, y)).x, _camera.ViewportToWorldPoint(new Vector3(x, y)).y);
        }

        public Vector2[] GetCameraBoundaryPoints()
        {
            return new Vector2[4]
            {
                GetCameraBoundaryPoint(0, 0),
                GetCameraBoundaryPoint(1, 0),
                GetCameraBoundaryPoint(0, 1),
                GetCameraBoundaryPoint(1, 1)
            };
        }

        public Vector2[] GetCameraVerticalLineBoundaryPoints(float x)
        {
            return new Vector2[2]{
                GetCameraBoundaryPoint(x, 0),
                GetCameraBoundaryPoint(x, 1),
            };
        }

        public Vector2[] GetCameraHorizontalLineBoundaryPoints(float y)
        {
            return new Vector2[2]{
                GetCameraBoundaryPoint(0, y),
                GetCameraBoundaryPoint(1, y),
            };
        }
    }
}
