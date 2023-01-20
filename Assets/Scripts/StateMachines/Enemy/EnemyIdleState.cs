using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash=Animator.StringToHash("Locomotion");
    private readonly int SpeedHash=Animator.StringToHash("Speed");
    private const float CrossFadeDuration=0.1f;
     private const float AnimatorDampTime=0.1f;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash,CrossFadeDuration);
       
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if(IsOnDetectionRange()){
            Debug.Log("I m seeing you");
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
         stateMachine.Animator.SetFloat(SpeedHash,0,AnimatorDampTime,deltaTime);
    }

    public override void Exit()
    {
        
    }

    
}
