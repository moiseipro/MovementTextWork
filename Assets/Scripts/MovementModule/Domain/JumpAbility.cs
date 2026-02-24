using MovementModule.Infrastructure;
using MovementModule.Input;

namespace MovementModule.Domain
{
    public class JumpAbility : IMovementAbility
    {
        private readonly CharacterConfig _config;
        private readonly IMovementExecutor _executor;
        private readonly IGroundSensor _groundSensor;

        private bool _isJumping;
        private bool _isHoldingJump;
        private bool _wasGroundedLastFrame;
        
        public JumpAbility(
            CharacterConfig config,
            IMovementExecutor executor,
            IGroundSensor groundSensor)
        {
            _config = config;
            _executor = executor;
            _groundSensor = groundSensor;
        }

        public void OnTick(float deltaTime, IMoveInputProvider input)
        {
            bool jumpPressed = input.IsJumpPressed();
            bool jumpHeld = input.IsJumpHeld();
            bool jumpReleased = input.IsJumpReleased();
            bool isGrounded = _groundSensor.IsGrounded;
            
            if (jumpPressed)
            {
                _groundSensor.NotifyJumpPressed();
                _isHoldingJump = true;
            }

            if (jumpReleased)
            {
                _isHoldingJump = false;
            }
            
            bool canJumpDirect = jumpPressed && (isGrounded || _groundSensor.CanCoyoteJump);
            bool canJumpBuffered = _groundSensor.HasJumpBuffer && isGrounded && !_wasGroundedLastFrame;

            if ((canJumpDirect || canJumpBuffered) && !_isJumping)
            {
                PerformJump();
                _isJumping = true;
            }
            
            _wasGroundedLastFrame = isGrounded;
            
            if (isGrounded)
            {
                _isJumping = false;
            }
        }

        private void PerformJump()
        {
            _executor.Jump(_config.JumpForce);
        }
    }
}