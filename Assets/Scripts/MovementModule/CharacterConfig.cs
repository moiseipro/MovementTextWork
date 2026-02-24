using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Game/CharacterConfig", order = 0)]
public class CharacterConfig : ScriptableObject
{
    [Header("Movement")]
    public float MaxSpeed = 5f;
    public float Acceleration = 50f;
    public float Deceleration = 50f;
    public float AirMaxSpeed = 4f;
    public float AirAcceleration = 30f;
    public float AirDeceleration = 10f;

    [Header("Jump")]
    public float JumpForce = 10f;
    
    [Header("Jump Feel")]
    public float CoyoteTime = 0.15f;
    public float JumpBufferTime = 0.1f;

    [Header("Wall Interaction")]
    public LayerMask WallLayer;
    public Vector2 WallCheckSize = new Vector2(0.1f, 0.8f);
    public Vector2 WallCheckOffset = new Vector2(0.5f, 0f);
    
    [Header("Checks")]
    public LayerMask GroundLayer;
    public Vector2 GroundCheckSize = new Vector2(1f, 0.1f);
}