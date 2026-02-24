
namespace MovementModule.Infrastructure
{
    public interface IMovementExecutor
    {
        void SetHorizontalVelocity(float velocity);
        void Jump(float force);
        float GetCurrentVerticalVelocity();
    }
}