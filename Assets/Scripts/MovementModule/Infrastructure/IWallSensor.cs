namespace MovementModule.Infrastructure
{
    public interface IWallSensor
    {
        bool IsTouchingWall { get; }
        bool IsTouchingWallRight { get; }
        bool IsTouchingWallLeft { get; }
    }
}