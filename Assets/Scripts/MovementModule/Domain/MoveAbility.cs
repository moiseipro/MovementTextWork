using MovementModule.Infrastructure;
using MovementModule.Input;
using UnityEngine;

namespace MovementModule.Domain
{
    public class MoveAbility : IMovementAbility
    {
        private readonly CharacterConfig _config;
        private readonly IMovementExecutor _executor;
        private readonly IGroundSensor _groundSensor;
        private readonly IWallSensor _wallSensor;
    
        private float _currentHorizontalVelocity;
        
        public MoveAbility(
            CharacterConfig config,
            IMovementExecutor executor,
            IGroundSensor groundSensor,
            IWallSensor wallSensor)
        {
            _config = config;
            _executor = executor;
            _groundSensor = groundSensor;
            _wallSensor = wallSensor;
        }

        public void OnTick(float deltaTime, IMoveInputProvider input)
        {
            float moveInput = input.GetMoveDirection().x;
            bool isGrounded = _groundSensor.IsGrounded;
            bool isTouchingWall = _wallSensor.IsTouchingWall;

            bool pushingIntoWall = false;
            if (isTouchingWall)
            {
                if (moveInput > 0 && _wallSensor.IsTouchingWallRight) pushingIntoWall = true;
                if (moveInput < 0 && _wallSensor.IsTouchingWallLeft) pushingIntoWall = true;
            }

            float maxSpeed = isGrounded ? _config.MaxSpeed : _config.AirMaxSpeed;
            float acceleration = isGrounded ? _config.Acceleration : _config.AirAcceleration;
            float deceleration = isGrounded ? _config.Deceleration : _config.AirDeceleration;
            
            float targetVelocity = moveInput * maxSpeed;
            
            if (!isGrounded && pushingIntoWall)
            {
                _currentHorizontalVelocity = 0f;
            }
            else
            {
                float rate = Mathf.Abs(targetVelocity) > Mathf.Abs(_currentHorizontalVelocity)
                    ? acceleration
                    : deceleration;

                _currentHorizontalVelocity = Mathf.MoveTowards(
                    _currentHorizontalVelocity,
                    targetVelocity,
                    rate * deltaTime);
            }

            _executor.SetHorizontalVelocity(_currentHorizontalVelocity);
        }
    }

}