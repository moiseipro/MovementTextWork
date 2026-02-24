using MovementModule.Input;

namespace MovementModule.Domain
{
    public interface IMovementAbility
    {
        void OnTick(float deltaTime, IMoveInputProvider input);
    }
}