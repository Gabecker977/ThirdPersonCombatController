using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
     private readonly int JumpHash=Animator.StringToHash("Jump");
    private const float CrossFadeDuration=0.1f;
    private Vector3 momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum= stateMachine.CharacterController.velocity;
        momentum.y=0f;
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash,CrossFadeDuration);
        
    }
    public override void Tick(float deltaTime)
    {
    Move(momentum,deltaTime);
    if(stateMachine.CharacterController.velocity.y<=0){
        stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        return;
    }
    FaceTarget();
    }

    public override void Exit()
    {
        
    }
}
