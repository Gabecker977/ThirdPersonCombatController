using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
  
  private readonly int TargetingBlendTreeHash=Animator.StringToHash("Targeting Blend Tree");
  private readonly int TargetingForwordHash=Animator.StringToHash("TargetingForword");
  private readonly int TargetingRightHash=Animator.StringToHash("TargetingRight");
  private const float CrossFadeDuration=0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      stateMachine.InputReader.TargetEvent+=OnCancel;
      stateMachine.InputReader.DodgeEvent+=OnDodge;
      stateMachine.InputReader.JumpEvent+=OnJump;
      stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash,CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
      if(stateMachine.InputReader.IsAttacking){
        stateMachine.SwitchState(new PlayerAttackState(stateMachine,0));
        return;
      }

       if(stateMachine.Targeter.CurrentTarget==null){
        stateMachine.SwitchState(new FreeLookState(stateMachine));
        return;
       }
       if(stateMachine.InputReader.IsBlocking){
        stateMachine.SwitchState(new PlayerBlockState(stateMachine));
       }
       Vector3 movement = CalculateMovement(deltaTime);
       Move(movement*stateMachine.TargetMovementSpeed,deltaTime);
       UpdateAnimator(deltaTime);
       FaceTarget();
       
    }

    public override void Exit()
    {
     stateMachine.InputReader.TargetEvent-=OnCancel;
     stateMachine.InputReader.DodgeEvent-=OnDodge;
     stateMachine.InputReader.JumpEvent-=OnJump;

    }
    private void OnCancel(){
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new FreeLookState(stateMachine));  
    }
    private void OnDodge(){
      if(stateMachine.InputReader.MovementValue==Vector2.zero) return;
      stateMachine.SwitchState(new PlayerDodgeState(stateMachine,stateMachine.InputReader.MovementValue));
    }
    private void OnJump(){
      stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private Vector3 CalculateMovement(float deltaTime){
      Vector3 movement =new Vector3();
      movement+=stateMachine.transform.right*stateMachine.InputReader.MovementValue.x;
      movement+=stateMachine.transform.forward*stateMachine.InputReader.MovementValue.y;
      return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
      if(stateMachine.InputReader.MovementValue.y==0){
        stateMachine.Animator.SetFloat(TargetingForwordHash,0,0.1f,deltaTime);
      }else{
         float value =stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
        stateMachine.Animator.SetFloat(TargetingForwordHash,value,0.1f,deltaTime);
      }
      if(stateMachine.InputReader.MovementValue.x==0){
        stateMachine.Animator.SetFloat(TargetingRightHash,0,0.1f,deltaTime);
      }else{
         float value =stateMachine.InputReader.MovementValue.x> 0 ? 1f : -1f;
        stateMachine.Animator.SetFloat(TargetingRightHash,value,0.1f,deltaTime);
      }
      
    } 
}
