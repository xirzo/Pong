using Pong.Domain.CameraHandling;
using UnityEngine;

namespace Pong.Domain.Spawning
{
    public class BallSpawner : MonoBehaviour, Spawner
    {
        private const float CENTER_OF_SCREEN_X = 0.5f;
        [SerializeField] private CameraBoundsCalculator _cameraBoundsCalculator;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _parent;

        private void Start()
        {
            Spawn();
        }

        public Spawnable Spawn()
        {
            var newBall = Instantiate(_ballPrefab, _parent);
            newBall.Initialize();
            return newBall;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (Application.isPlaying == true)
            {
                var points = _cameraBoundsCalculator.GetCameraVerticalLineBoundaryPoints(CENTER_OF_SCREEN_X);
                Gizmos.DrawLine(points[0], points[1]);
            }
        }
    }
}
