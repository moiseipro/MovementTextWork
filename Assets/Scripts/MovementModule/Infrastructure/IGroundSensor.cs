namespace MovementModule.Infrastructure
{
    public interface IGroundSensor
    {
        bool IsGrounded { get; }
        bool CanCoyoteJump { get; }
        bool HasJumpBuffer { get; }
        void NotifyJumpPressed();
    }
}