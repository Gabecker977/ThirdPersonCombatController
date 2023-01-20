using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.WeaponDamege.gameObject.SetActive(false);
    }
     public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {
       
    }
   
}
