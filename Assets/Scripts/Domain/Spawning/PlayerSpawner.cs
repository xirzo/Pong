using Pong.Domain.CameraHandling;
using Pong.Domain.Spawning.Entities;
using UnityEngine;

namespace Pong.Domain.Spawning
{
    public class PlayerSpawner : MonoBehaviour, Spawner
    {
        [SerializeField, Range(0f, 1f)] private float _spawnLinePosition = 0.25f;
        [SerializeField] private CameraBoundsCalculator _cameraBoundsCalculator;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _parent;

        private void Start()
        {
            Spawn();
        }

        private Vector3 GetLineMedianPoint()
        {
            var points = _cameraBoundsCalculator.GetCameraVerticalLineBoundaryPoints(_spawnLinePosition);
            var medianPoint = new Vector3(points[0].x, Mathf.Abs(points[0].y) - Mathf.Abs(points[1].y));
            return medianPoint;
        }

        public Spawnable Spawn()
        {
            var newPlayer = Instantiate(_playerPrefab, GetLineMedianPoint(), Quaternion.identity, _parent);
            newPlayer.Initialize();
            return newPlayer;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (Application.isPlaying == true)
            {
                var points = _cameraBoundsCalculator.GetCameraVerticalLineBoundaryPoints(_spawnLinePosition);
                Gizmos.DrawLine(points[0], points[1]);
            }
        }
    }
}
