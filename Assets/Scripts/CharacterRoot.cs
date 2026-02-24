using System.Collections.Generic;
using MovementModule.Domain;
using MovementModule.Infrastructure;
using MovementModule.Input;
using UnityEngine;

public class CharacterRoot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private PlayerMoveInputProvider _inputProvider;
    [SerializeField] private RigidbodyMovementExecutor _executor;
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private WallSensor _wallSensor;
    
    [Header("View")]
    [SerializeField] private CharacterView _view;

    private List<IMovementAbility> _abilities = new();

    private void Awake()
    {
        _abilities.Add(new MoveAbility(_config, _executor, _groundSensor, _wallSensor));
        _abilities.Add(new JumpAbility(_config, _executor, _groundSensor));
            
        _groundSensor.OnGroundedChanged += _view.HandleGrounded;
        _wallSensor.OnTouchingWallChanged += _view.HandleTouchingWall;
    }

    private void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
            
        foreach (var ability in _abilities)
        {
            ability.OnTick(deltaTime, _inputProvider);
        }
        
        _view.HandleVelocity(_executor.Velocity);
    }
}