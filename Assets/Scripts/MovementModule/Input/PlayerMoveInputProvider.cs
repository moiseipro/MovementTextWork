using UnityEngine;

namespace MovementModule.Input
{
    public class PlayerMoveInputProvider: MonoBehaviour, IMoveInputProvider
    {
        private PlayerInputActions _inputActions;

        private Vector2 _moveDirection;
        private bool _jumpHeld;
        private bool _jumpPressed;
        private bool _jumpReleased;
        
        private void Awake() => _inputActions = new PlayerInputActions();

        private void Update()
        {
            _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>();
            _jumpHeld = _inputActions.Player.Jump.IsPressed();
            
            if (_inputActions.Player.Jump.WasPressedThisFrame())
                _jumpPressed = true;

            if (_inputActions.Player.Jump.WasReleasedThisFrame())
                _jumpReleased = true;
        }

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();

        public Vector2 GetMoveDirection() => _moveDirection;
        public bool IsJumpHeld() => _jumpHeld;
        public bool IsJumpPressed()
        {
            var jumpPressed = _jumpPressed;
            _jumpPressed = false;
            return jumpPressed;
        }

        public bool IsJumpReleased()
        {
            var jumpReleased = _jumpReleased;
            _jumpReleased = false;
            return jumpReleased;
        }
    }
}