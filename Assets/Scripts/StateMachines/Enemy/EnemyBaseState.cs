using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine){
        this.stateMachine=stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime){
        stateMachine.CharacterController.Move((motion+stateMachine.ForceReceiver.Movement)*deltaTime);
    }
     protected void Move(float deltaTime){
        Move(Vector3.zero,deltaTime);
     }
     protected void FacePlayer()
     {
    if(stateMachine.Player==null) return;
        Vector3 lookDir=stateMachine.Player.transform.position-stateMachine.transform.position;
       lookDir.y=0;
       stateMachine.transform.rotation=Quaternion.LookRotation(lookDir);
     }
    protected bool IsOnDetectionRange(){
        if(stateMachine.Player.isDead) return false;
        float distanceToPlayer=(stateMachine.Player.transform.position-stateMachine.transform.position).sqrMagnitude;
        
        return distanceToPlayer<=stateMachine.DetectionRange*stateMachine.DetectionRange;
    }
     

}
