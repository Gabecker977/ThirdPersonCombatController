using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]public InputReader InputReader{get;private set;}
    [field: SerializeField]public CharacterController CharacterController{get;private set;}
    [field: SerializeField]public Animator Animator{get;private set;}
    [field: SerializeField]public Targeter Targeter{get;private set;}
    [field: SerializeField] public Health Health{get;private set;}
    [field: SerializeField]public ForceReceiver ForceReceiver{get;private set;}
    [field: SerializeField]public float FreeLookMovementSpeed{get;private set;}
    [field: SerializeField]public float TargetMovementSpeed{get;private set;}
    [field: SerializeField]public float RotationSpeed{get;private set;}
     [field: SerializeField]public float DodgeDuration{get;private set;}
     [field: SerializeField]public float DodgeDistance{get;private set;}
     [field: SerializeField]public float JumpForce{get;private set;}
    [field: SerializeField]public Attack[] Attacks{get;private set;}
    [field: SerializeField]public WeaponDamege WeaponDamege{get;private set;}
    [field: SerializeField]public Ragdoll Ragdoll{get;private set;}
     [field: SerializeField]public LedgeDetector LedgeDetector{get;private set;}
    public Transform MainCameraTransform{get;private set;}
    public float DodgePreviusTime{get;private set;}=Mathf.NegativeInfinity;
    private void Awake() {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
    }
   private void Start() {
    MainCameraTransform=Camera.main.transform;
    SwitchState(new FreeLookState(this));
   }
    
    private void OnEnable() {
        Health.OnTakeDamege+=HandleTakeDamege;
        Health.OnDie+=HandleDie;
    }
    private void OnDisable() {
        Health.OnTakeDamege-=HandleTakeDamege;
        Health.OnDie-=HandleDie;
    }
    private void HandleTakeDamege(){
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDie(){
        SwitchState(new PlayerDeathState(this));
    }
}
