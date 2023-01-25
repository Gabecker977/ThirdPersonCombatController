using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //toggle rogdoll
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Ragdoll.ApplyHipsForce(50f);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }
    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {

    }
}
