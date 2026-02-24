using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
        
    public void HandleGrounded(bool isGrounded)
    {
        _animator.SetBool("IsGrounded", isGrounded);
    }

    public void HandleTouchingWall(bool isTouchingWall)
    {
        _animator.SetBool("IsTouchingWall", isTouchingWall);
    }

    public void HandleVelocity(Vector2 vel)
    {
        _animator.SetFloat("SpeedX", Mathf.Abs(vel.x));
        _animator.SetFloat("SpeedY", vel.y);

        if (vel.x < 0)
        {
            _spriteRenderer.flipX = true;
        } else if (vel.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}