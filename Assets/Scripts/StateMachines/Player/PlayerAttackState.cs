using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{

    private Attack attack;
    private bool forceAppled;
    public PlayerAttackState(PlayerStateMachine stateMachine,int attackIndex) : base(stateMachine)
    {
        attack=stateMachine.Attacks[attackIndex];
        stateMachine.WeaponDamege.SetWeaponDamege(attack.Damege,attack.Knockback);
    }

    public override void Enter()
    {
      stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,attack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime =GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime>=attack.ForceTime){
            TryApplyForce();
        }

        if(normalizedTime<1){
            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }else{
            if(stateMachine.Targeter.SelectCurrentTarget())
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else
                stateMachine.SwitchState(new FreeLookState(stateMachine));
        }
       
    }

    public override void Exit()
    {
        
    }
    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboStateIndex==-1)return;

        if(normalizedTime<attack.ComboAttackTime) return;
        stateMachine.SwitchState(new PlayerAttackState(stateMachine,attack.ComboStateIndex));
    }
    private void TryApplyForce(){
        if(forceAppled) return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward*attack.Force);
        forceAppled=true;
    }
   

    
}
