using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash=Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwordHash=Animator.StringToHash("DodgeRight");
    private readonly int DodgeRightHash=Animator.StringToHash("DodgeForward");
    private Vector2 dodgingDirectionInput;
    private float dodgeTime;
    private const float CrossFadeDuration=0.1f;
    public PlayerDodgeState(PlayerStateMachine stateMachine,Vector3 dodgingDirectionInput) : base(stateMachine)
    {
    this.dodgingDirectionInput=dodgingDirectionInput;
    }

    public override void Enter()
    {
        dodgeTime=stateMachine.DodgeDuration;
        stateMachine.Animator.SetFloat(DodgeRightHash,dodgingDirectionInput.x);
        stateMachine.Animator.SetFloat(DodgeForwordHash,dodgingDirectionInput.y);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash,CrossFadeDuration);
    stateMachine.Health.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement=new Vector3();
        movement+=stateMachine.transform.right * dodgingDirectionInput.x*stateMachine.DodgeDistance/stateMachine.DodgeDuration;
        movement+=stateMachine.transform.forward * dodgingDirectionInput.y*stateMachine.DodgeDistance/stateMachine.DodgeDuration;
        Move(movement,deltaTime);
        
        FaceTarget();

        dodgeTime-=deltaTime;

        if(dodgeTime<=0f){
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    
}
