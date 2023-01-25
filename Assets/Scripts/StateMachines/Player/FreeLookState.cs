using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookState : PlayerBaseState
{
    private readonly int freeLookBlendTreeHash=Animator.StringToHash("FreeLookBlendTree");
   private readonly int freeLookSpeedHash=Animator.StringToHash("FreeLookSpeed");
   private const float AnimatorDampTime=0.1f;
   private const float CrossFadeDuration=0.1f;
   private bool shouldFade;
 
    public FreeLookState(PlayerStateMachine stateMachine, bool shouldFade=true) : base(stateMachine)
    {
    this.shouldFade=shouldFade;
    }

    public override void Enter()
    {
       
        stateMachine.InputReader.TargetEvent+=OnTarget;
        stateMachine.InputReader.JumpEvent+=OnJump;
        stateMachine.Animator.SetFloat(freeLookSpeedHash,0f);
        if(shouldFade)
            stateMachine.Animator.CrossFadeInFixedTime(freeLookBlendTreeHash,CrossFadeDuration);
        else{
            stateMachine.Animator.Play(freeLookBlendTreeHash);
        }
    }
    
    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking){
        stateMachine.SwitchState(new PlayerAttackState(stateMachine,0));
        return;
      }
        Vector3 dir = CalculateMovement();

        Move(dir*stateMachine.FreeLookMovementSpeed,deltaTime);
        //If is not moving
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 0,AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(freeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(dir,deltaTime);
    }
    public override void Exit()
    {
       stateMachine.InputReader.TargetEvent-=OnTarget;
        stateMachine.InputReader.JumpEvent-=OnJump;
    }
    private void FaceMovementDirection(Vector3 dir,float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, 
        Quaternion.LookRotation(dir),deltaTime*stateMachine.RotationSpeed);
    }
    private Vector3 CalculateMovement(){
        Vector3 forword= stateMachine.MainCameraTransform.forward;
        Vector3 right= stateMachine.MainCameraTransform.right;

        forword.y=0;
        right.y=0;

        forword.Normalize();
        right.Normalize();

        return forword*stateMachine.InputReader.MovementValue.y +
        right*stateMachine.InputReader.MovementValue.x;
    }  
    private void OnTarget(){
        if(!stateMachine.Targeter.SelectClosestTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
     private void OnJump(){
      stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
   
}
