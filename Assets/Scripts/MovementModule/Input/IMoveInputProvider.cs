using UnityEngine;

namespace MovementModule.Input
{
    public interface IMoveInputProvider
    {
        Vector2 GetMoveDirection();
        bool IsJumpPressed();
        bool IsJumpHeld();
        bool IsJumpReleased();
    }
}