using System;
using UnityEngine;

namespace MovementModule.Infrastructure
{
    public class WallSensor : MonoBehaviour, IWallSensor
    {
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private Transform _checkPoint;

        public bool IsTouchingWall { get; private set; }
        public bool IsTouchingWallRight { get; private set; }
        public bool IsTouchingWallLeft { get; private set; }
        
        public event Action<bool> OnTouchingWallChanged;

        private void FixedUpdate()
        {
            IsTouchingWallRight = Physics2D.OverlapBox(
                _checkPoint.position + Vector3.right * _config.WallCheckOffset.x + Vector3.up * _config.WallCheckOffset.y,
                _config.WallCheckSize, 
                0, 
                _config.WallLayer
            );
            
            IsTouchingWallLeft = Physics2D.OverlapBox(
                _checkPoint.position + Vector3.left * _config.WallCheckOffset.x + Vector3.up * _config.WallCheckOffset.y, 
                _config.WallCheckSize, 
                0, 
                _config.WallLayer
            );

            IsTouchingWall = IsTouchingWallRight || IsTouchingWallLeft;
            OnTouchingWallChanged?.Invoke(IsTouchingWall);
        }

        private void OnDrawGizmos()
        {
            if (!_checkPoint) return;
            Gizmos.color = IsTouchingWall ? Color.red : Color.blue;
            Gizmos.DrawWireCube(_checkPoint.position + Vector3.right * _config.WallCheckOffset.x + Vector3.up * _config.WallCheckOffset.y, _config.WallCheckSize);
            Gizmos.DrawWireCube(_checkPoint.position + Vector3.left * _config.WallCheckOffset.x + Vector3.up * _config.WallCheckOffset.y, _config.WallCheckSize);
        }
    }
}