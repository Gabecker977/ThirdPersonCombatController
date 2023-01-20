using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandle : MonoBehaviour
{
   [SerializeField] private GameObject SwordHitBox;

   public void EnableWeapon(){
    SwordHitBox.SetActive(true);
   }
   public void DisableWeapon(){
    SwordHitBox.SetActive(false);
   }
}
