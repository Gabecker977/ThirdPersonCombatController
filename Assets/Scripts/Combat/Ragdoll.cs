using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Rigidbody hipsRb;
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;
    void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRagdoll(false);
    }


    public void ToggleRagdoll(bool isRagdoll){
        foreach(Collider collider in colliders){
            if(collider.gameObject.CompareTag("Ragdoll")){
                collider.enabled=isRagdoll;
            }
        }
        foreach(Rigidbody rigidbody in rigidbodies){
            if(rigidbody.gameObject.CompareTag("Ragdoll")){
                rigidbody.isKinematic=!isRagdoll;
                rigidbody.useGravity=isRagdoll;
            }
        }
        characterController.enabled=!isRagdoll;
        animator.enabled=!isRagdoll;
    }
    public void ApplyHipsForce(float force){
        if(hipsRb==null) return;
        hipsRb.velocity=-hipsRb.transform.forward*force;
    }
}
