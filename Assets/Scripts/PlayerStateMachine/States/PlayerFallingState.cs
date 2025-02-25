using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerState
{

    private float _currentFallingSpeed = 0f;
    private float _acceleration = 9.81f;
    public PlayerFallingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    
    public override void EnterState()
    {
        _currentFallingSpeed = 0f;
    }
    
    public override void Update()
    {
        _currentFallingSpeed += _acceleration * Time.deltaTime;
        
        player._characterController.Move(Physics.gravity * Time.deltaTime * _currentFallingSpeed);
        if (player._characterController.isGrounded)
        {
            player._stateMachine.ChangePlayerState(player._movingState);
        }
    }
}
