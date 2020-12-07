using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LanternBoss : MonoBehaviour
{
   public int health;
   public Enemy[] enemies;
   public float spawnOffset;

   private int halfHealth;
   private Animator anim;
   public int damage;

   public float timeBetweenSummons;
   private float summonTime;

   private float attackTime;
   public float timeBetweenAttacks;

   private bool canTakeDamage = true;
   public float damageTimeout;
   

   public Transform shotPoint;
   public Transform shotPoint1;
   public GameObject enemyBullet;

   public GameObject effect;
   public GameObject morphEffect;
   

   [HideInInspector]
   public Transform player;

  private Slider healthBar;

  private SceneTransition sceneTransitions;

   private void Start()
   {
   	player = GameObject.FindGameObjectWithTag("Player").transform;

   	halfHealth = health / 2;
   	
   	anim = GetComponent<Animator>();

    healthBar = FindObjectOfType<Slider>();
    healthBar.maxValue = health;
    healthBar.value = health;

    // Changing scene when boss dies
    sceneTransitions = FindObjectOfType<SceneTransition>();
   }


   private void Update()
   {
   		if(Time.time >= summonTime)
		{
			summonTime = Time.time + timeBetweenSummons;
			
			if(health <= halfHealth)
			{
                Instantiate(morphEffect, transform.position, Quaternion.identity);
				Summon();
			}   			
		}

		if(Time.time >= attackTime)
		    {
		        //enemy attacks the player
		        RangedAttack();	
		        attackTime = Time.time + timeBetweenAttacks;
            
		            
                
		    }

        // Boss's Range Attack
		void RangedAttack()
    	{
            
                Vector2 direction = player.position - shotPoint.position;   
                Vector2 direction1 = player.position - shotPoint1.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                Quaternion rotation1 = Quaternion.AngleAxis(angle1 - 90, Vector3.forward);

                shotPoint.rotation = rotation;
                shotPoint1.rotation = rotation1;

                Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
                Instantiate(enemyBullet, shotPoint1.position, shotPoint1.rotation);
                
        	
    	}
   }

    // Boss Takes Damage
   public void TakeDamage(int damageAmount){
       if(canTakeDamage){
        
        health -= damageAmount;
   		healthBar.value = health;
       }
        
   		
    //When half health, boss changes form and moves fast
    // if (currentTime <= 0) {
    //         Debug.Log ("Damage");
 
    //         currentTime = nextDamage;
    //     } else {
     
    //         currentTime -= Time.deltaTime;
    //     }
        
      if(health <= halfHealth)
      {
            
            StartCoroutine(damageTimer());
        
            anim.SetTrigger("Stage2");
            anim.SetTrigger("moveFast");
            
     
        // Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];  
        // Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        
      }


   		if(health <= 0)
   		{
   			Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
   			Instantiate(effect, transform.position, Quaternion.identity);

        //when boss's health is 0 change the scene
        sceneTransitions.LoadScene("Win");
   		}

   		

   		
   	
   		
    }

    // Boss Summons Enemy
    public void Summon()
   {
   		if(player != null)
   		{
   			Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];	
   			Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
   		}
   }


    // On Hit Player Takes Damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		collision.GetComponent<Player>().TakeDamage(damage);
    	}
    }

IEnumerator damageTimer() {
    canTakeDamage = false;
    yield return new WaitForSeconds(damageTimeout);
    canTakeDamage = true;
}


    

}
