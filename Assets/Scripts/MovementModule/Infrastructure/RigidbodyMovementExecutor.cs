using UnityEngine;

namespace MovementModule.Infrastructure
{
    public class RigidbodyMovementExecutor : MonoBehaviour, IMovementExecutor
    {
        private Rigidbody2D _rb;
        
        public Vector2 Velocity => _rb.linearVelocity;
        
        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        public void SetHorizontalVelocity(float velocity)
        {
            _rb.linearVelocity = new Vector2(velocity, _rb.linearVelocity.y);
        }

        public void Jump(float force)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, force);
        }

        public float GetCurrentVerticalVelocity() => _rb.linearVelocity.y;
    }
}