using System;
using UnityEngine;

namespace MovementModule.Infrastructure
{
    public class GroundSensor : MonoBehaviour, IGroundSensor
    {
        [SerializeField] private Transform _checkPoint;
        [SerializeField] private CharacterConfig _config;
    
        public bool IsGrounded { get; private set; }
        public bool CanCoyoteJump { get; private set; }
        public bool HasJumpBuffer { get; private set; }

        private float _coyoteTimer;
        private float _bufferTimer;
        
        public event Action<bool> OnGroundedChanged;

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapBox(_checkPoint.position, _config.GroundCheckSize, 0, _config.GroundLayer);
            OnGroundedChanged?.Invoke(IsGrounded);
            
            if (IsGrounded)
                _coyoteTimer = _config.CoyoteTime;
            else if (_coyoteTimer > 0)
                _coyoteTimer -= Time.fixedDeltaTime;
        
            CanCoyoteJump = _coyoteTimer > 0;
            
            if (_bufferTimer > 0)
                _bufferTimer -= Time.fixedDeltaTime;
        
            HasJumpBuffer = _bufferTimer > 0;
        }
        
        public void NotifyJumpPressed()
        {
            _bufferTimer = _config.JumpBufferTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = CanCoyoteJump ? Color.yellow : Color.red;
            Gizmos.DrawWireCube(_checkPoint.position, _config.GroundCheckSize);
        }
    }
}