using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash=Animator.StringToHash("Locomotion");
    private readonly int SpeedHash=Animator.StringToHash("Speed");
    private const float CrossFadeDuration=0.1f;
    private const float AnimatorDampTime=0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash,CrossFadeDuration);
    }
     public override void Tick(float deltaTime)
    {
       if(!IsOnDetectionRange()){
        stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        return;
       }else if(IsOnAttackRange()){
        stateMachine.SwitchState(new EnemyAttackState(stateMachine));
        return;
       }
        
        MoveToPlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash,1,AnimatorDampTime,deltaTime);
    }

    public override void Exit()
    {
       stateMachine.Agent.ResetPath();
       stateMachine.Agent.velocity=Vector3.zero;
    }
    private void MoveToPlayer(float deltaTime){
        if(stateMachine.Agent.isOnNavMesh){
        stateMachine.Agent.destination = stateMachine.Player.transform.position;
        Move(stateMachine.Agent.desiredVelocity.normalized*stateMachine.MovementSpeed,deltaTime);
        }
       stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
    }
    private bool IsOnAttackRange(){
        if(stateMachine.Player.isDead){return false;}
        float distanceToPlayer=(stateMachine.Player.transform.position-stateMachine.transform.position).sqrMagnitude;
        
        return distanceToPlayer<=stateMachine.AttckRange*stateMachine.AttckRange;
    }
}
