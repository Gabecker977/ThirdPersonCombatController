using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int ClimbingUoHash=Animator.StringToHash("Climbing");
    private const float CrossFadeDuration=0.1f;
    private readonly Vector3 OffSet=new Vector3(-0.147017851f,3.18574786f,0.602816045f);
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
       stateMachine.Animator.CrossFadeInFixedTime(ClimbingUoHash,CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator,"Climb")<1f) return;
        stateMachine.CharacterController.enabled=false;
        stateMachine.transform.Translate(OffSet,Space.Self);
        stateMachine.CharacterController.enabled=true;
        stateMachine.SwitchState(new FreeLookState(stateMachine,false));
    }

    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.ResetGravity();
    }

    
}
