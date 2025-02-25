using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
{
    private Vector3 _moveDir;
    public PlayerMovingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    
    public override void Update()
    {
        player.UpdateMovement();
        
        if (!player._characterController.isGrounded && player.howManyDetectingEdge() == 0)
        {
            player._stateMachine.ChangePlayerState(player._fallingState);
        }
    }
    

}

