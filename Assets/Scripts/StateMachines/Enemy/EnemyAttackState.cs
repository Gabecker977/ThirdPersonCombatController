using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int AttackHash=Animator.StringToHash("Attack");
    private const float CrossFadeDuration=0.1f;
    private const float AnimatorDampTime=0.1f;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    
    }

    public override void Enter()
    {
        FacePlayer();
        stateMachine.Weapon.SetWeaponDamege(stateMachine.AttckDamege,stateMachine.AttckKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash,CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator)>=1){
        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
