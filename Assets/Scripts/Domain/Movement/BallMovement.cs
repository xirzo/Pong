using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Pong.Domain.Movement
{
    public class BallMovement : IInitializable, IFixedTickable
    {
        private readonly float _speed = 100f;
        private readonly float _maximumVelocity = 10f;

        private const float MinAngle = -35f;
        private const float MaxAngle = 30f;
        private const float MinHorizontalComponent = 0.5f;
        private const float MaxVerticalAngle = 75f; 
        private const float SpeedDecay = 0.98f;
        private const float MinSpeed = 50f; 
        private const float MaxSpeed = 150f;

        private readonly Rigidbody2D _rigidbody;

        private Vector2 _direction;
        private float _currentSpeed;
        private bool _isInitialized;

        public BallMovement(float speed, float maximumVelocity, Rigidbody2D rigidbody)
        {
            _speed = speed;
            _maximumVelocity = maximumVelocity;
            _rigidbody = rigidbody;
            _currentSpeed = speed;
        }

        public void FixedTick()
        {
            if (_isInitialized)
            {
                Move();
            }
        }

        public void Initialize()
        {
            _currentSpeed = _speed;
            _isInitialized = false;
            SetRandomDirection();
            _isInitialized = true;
        }

        public void ResetVelocity()
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _currentSpeed = _speed;
        }

        public void SetRandomDirection()
        {
            var randomAngle = Random.Range(MinAngle, MaxAngle);
            var angleRad = randomAngle * Mathf.Deg2Rad;
            
            var direction = new Vector2(
                Mathf.Cos(angleRad),
                Mathf.Sin(angleRad)
            );
            
            if (Mathf.Abs(direction.x) < MinHorizontalComponent)
            {
                direction.x = MinHorizontalComponent * Mathf.Sign(direction.x);
                direction = direction.normalized;
            }
            
            if (Random.value < 0.5f)
            {
                direction.x = -direction.x;
            }
            
            _direction = direction.normalized;
        }

        public void CalculateReflectionAndSetDirection(Vector2 normal)
        {
            normal = normal.normalized;
            
            var reflected = Vector2.Reflect(_direction.normalized, normal);
            
            var angle = Mathf.Atan2(reflected.y, Mathf.Abs(reflected.x)) * Mathf.Rad2Deg;
            if (angle > MaxVerticalAngle)
            {
                var clampedAngle = MaxVerticalAngle * Mathf.Deg2Rad;
                var signX = Mathf.Sign(reflected.x);
                var signY = Mathf.Sign(reflected.y);
                
                reflected = new Vector2(
                    signX * Mathf.Cos(clampedAngle),
                    signY * Mathf.Sin(clampedAngle)
                );
            }
            
            if (Mathf.Abs(reflected.x) < MinHorizontalComponent)
            {
                reflected.x = MinHorizontalComponent * Mathf.Sign(reflected.x);
                reflected = reflected.normalized;
            }
            
            _direction = reflected.normalized;
            
            _currentSpeed = Mathf.Clamp(_currentSpeed * 1.02f, MinSpeed, MaxSpeed);
        }

        public void OnWallHit()
        {
            _currentSpeed = Mathf.Max(_currentSpeed * SpeedDecay, MinSpeed);
        }

        private void Move()
        {
            var targetVelocity = _direction * _currentSpeed;
            
            var clampedVelocity = Vector2.ClampMagnitude(targetVelocity, _maximumVelocity);
            
            _rigidbody.linearVelocity = clampedVelocity;
            
            if (_rigidbody.linearVelocity.magnitude > 0.1f)
            {
                _direction = _rigidbody.linearVelocity.normalized;
            }
        }

        public Vector2 GetCurrentDirection() => _direction;
        public float GetCurrentSpeed() => _currentSpeed;
        
        public void SetSpeed(float newSpeed)
        {
            _currentSpeed = Mathf.Clamp(newSpeed, MinSpeed, MaxSpeed);
        }
    }
}
