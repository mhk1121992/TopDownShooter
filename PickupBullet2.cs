using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBullet2 : MonoBehaviour
{
    public WeaponBullet2 weaponToEquip;
    public GameObject effect;

   private void OnTriggerEnter2D(Collider2D collision)
   {
   	if(collision.tag == "Player")
   	{
   		Instantiate(effect, transform.position, Quaternion.identity);
   		collision.GetComponent<Player>().ChangeWeapon2(weaponToEquip);
   		Destroy(gameObject);
   	}
   }
    
}
