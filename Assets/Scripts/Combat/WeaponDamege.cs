using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamege : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    private List<Collider> colliders = new List<Collider>();
    private int damege;
    private float knockback;
    private void OnEnable() {
        colliders.Clear();
    }
    private void OnTriggerEnter(Collider other) {
        if(other==_collider) return;

        if(colliders.Contains(other)) return;

        colliders.Add(other);

        if(other.TryGetComponent<Health>(out Health health)){
            health.DealDamege(damege);
        }
         if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver)){
            forceReceiver.AddForce((other.transform.position-_collider.transform.position).normalized
            *knockback);
         }
    }
    public void SetWeaponDamege(int damege, float knockback){
        this.damege=damege;
        this.knockback=knockback;
    }
}
