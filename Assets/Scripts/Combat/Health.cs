using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth=100;
    [SerializeField] private bool isInvulnerable=false;
    private int health;
    public event Action OnTakeDamege;
    public event Action OnDie;
    public bool isDead=>health==0;

    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInvulnerable(bool isInvunerable){
        this.isInvulnerable =isInvunerable;
    }
    public void DealDamege(int damege){
        if(health==0) {return;}
        if(isInvulnerable){return;}
        health=Mathf.Max(health-damege,0);
        OnTakeDamege?.Invoke();
        
        if(health==0){
            OnDie?.Invoke();
        }
        Debug.Log(gameObject.name+" : "+health);
    }
}
