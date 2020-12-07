using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

	[HideInInspector]
	public Transform player;

	public float speed;

	

	public int damage;

  public int pickupChance;
  public GameObject[] pickups;

  public int healthPickupChance;
  public GameObject healthPickup;

  public GameObject deathEffect;
  	
  public virtual void Start()
  {
  	player = GameObject.FindGameObjectWithTag("Player").transform;

  }


   public void TakeDamage(int damageAmount){
   		health -= damageAmount;

   		if(health <= 0)
   		{
			// Pickup
	        int randomNumber = Random.Range(0, 101);
	        if (randomNumber <pickupChance)
	        {
	          GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
	          Instantiate(randomPickup, transform.position, transform.rotation);
	        }

			// Health pickup
	        int randHealth = Random.Range(0, 101);
	        if(randHealth < healthPickupChance)
	        {
	        	Instantiate(healthPickup, transform.position, transform.rotation);
	        }

	        Instantiate(deathEffect, transform.position, Quaternion.identity);
   			Destroy(gameObject);
   		}
    }
}
