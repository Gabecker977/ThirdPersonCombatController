using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharacterController{get;private set;}
    [field: SerializeField] public ForceReceiver ForceReceiver{get;private set;}
    [field: SerializeField] public Animator Animator{get;private set;}
    [field: SerializeField] public NavMeshAgent Agent{get;private set;}
    [field: SerializeField] public Health Health{get;private set;}
    [field: SerializeField] public Target Target{get;private set;}
    [field: SerializeField] public float MovementSpeed{get;private set;}
    [field: SerializeField] public float DetectionRange{get;private set;}
    [field: SerializeField] public WeaponDamege Weapon{get;private set;}
    [field: SerializeField] public int AttckDamege{get;private set;}
    [field: SerializeField] public float AttckRange{get;private set;}
    [field: SerializeField] public float AttckKnockback{get;private set;}
    [field: SerializeField] public Ragdoll Ragdoll{get;private set;}
    public Health Player{get;private set;}
    private void Start() {
        Agent.updatePosition=false;
        Agent.updateRotation=false;

        Player =GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        SwitchState(new EnemyIdleState(this));
    }
    private void OnEnable() {
        Health.OnTakeDamege+=HandleTakeDamege;
        Health.OnDie+=HandleDie;
    }
    private void OnDisable() {
        Health.OnTakeDamege-=HandleTakeDamege;
        Health.OnDie-=HandleDie;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,DetectionRange);
    }
    private void HandleTakeDamege(){
        SwitchState(new EnemyImpactState(this));
    }
     private void HandleDie(){
        SwitchState(new EnemyDeathState(this));
    }
}
