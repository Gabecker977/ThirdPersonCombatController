using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine){
       this.stateMachine =stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime){
        stateMachine.CharacterController.Move((motion+stateMachine.ForceReceiver.Movement)*deltaTime);
    }
     protected void Move(float deltaTime){
        Move(Vector3.zero,deltaTime);
     }
    protected void FaceTarget(){
        if(stateMachine.Targeter.CurrentTarget==null) return;
        Vector3 lookDir=stateMachine.Targeter.CurrentTarget.transform.position-stateMachine.transform.position;
       lookDir.y=0;
       stateMachine.transform.rotation=Quaternion.LookRotation(lookDir);
    }
    protected void ReturnToLocomotion(){
        if(stateMachine.Targeter.SelectCurrentTarget()){
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else{
            stateMachine.SwitchState(new FreeLookState(stateMachine));
        }
    }
}
