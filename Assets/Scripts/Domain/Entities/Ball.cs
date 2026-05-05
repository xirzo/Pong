using System;
using Pong.Domain.Movement;
using Pong.Domain.Score;
using UnityEngine;

namespace Pong.Domain.Entities
{
    public class Ball : MonoBehaviour, ILoser
    {
        private const float HitCooldown = 0.05f;

        public event Action OnCollide;

        [SerializeField] private LayerMask repulseLayer;
        [SerializeField] private LayerMask wallLayer;

        private BallMovement _ballMovement;
        private Vector3 _startPosition;
        private EntityId? _lastHitPaddleId;
        private float _lastHitTime;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            bool isWall = ((1 << other.gameObject.layer) & wallLayer) != 0;

            if (isWall)
            {
                _ballMovement.OnWallHit();
                _ballMovement.CalculateReflectionAndSetDirection(other.contacts[0].normal);
                OnCollide?.Invoke();
                return;
            }
            
            if (((1 << other.gameObject.layer) & repulseLayer) == 0)
            {
                return;
            }

            if (!IsValidPaddleHit(other))
            {
                return;
            }

            if (IsDuplicateHit(other))
            {
                return;
            }

            OnCollide?.Invoke();

            _ballMovement.CalculateReflectionAndSetDirection(other.contacts[0].normal);
        }

        private bool IsValidPaddleHit(Collision2D other)
        {
            Vector2 ballDirection = _ballMovement.GetCurrentDirection();

            float paddlePositionX = other.transform.position.x;
            float paddleFacingDirection = paddlePositionX < 0 ? 1f : -1f;

            bool isMovingTowardFrontFace = Mathf.Approximately(Mathf.Sign(ballDirection.x), -paddleFacingDirection);

            if (!isMovingTowardFrontFace)
            {
                Vector3 pos = transform.position;
                pos.x += paddleFacingDirection * 0.1f;
                transform.position = pos;
                return false;
            }

            return true;
        }

        private bool IsDuplicateHit(Collision2D other)
        {
            EntityId paddleId = other.gameObject.GetEntityId();
            float currentTime = Time.time;

            if (_lastHitPaddleId.HasValue && paddleId == _lastHitPaddleId.Value &&
                currentTime - _lastHitTime < HitCooldown)
            {
                return true;
            }

            _lastHitPaddleId = paddleId;
            _lastHitTime = currentTime;
            return false;
        }

        public void Reset()
        {
            transform.position = _startPosition;
            _ballMovement.ResetVelocity();
            _ballMovement.SetRandomDirection();
            _lastHitPaddleId = null;
            _lastHitTime = 0f;
        }

        public void Construct(BallMovement ballMovement)
        {
            _ballMovement = ballMovement;
        }
    }
}