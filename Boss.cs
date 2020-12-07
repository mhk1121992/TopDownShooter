using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
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
   public Transform shotPoint1;
   public Transform shotPoint2;
   public GameObject enemyBullet;

   public GameObject effect;
   

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
				Summon();
			}   			
		}

		if(Time.time >= attackTime)
		    {
		        //enemy attacks the player
		        		
		        attackTime = Time.time + timeBetweenAttacks;
		        RangedAttack();
		    }


		void RangedAttack()
    	{
    		Vector2 direction1 = player.position - shotPoint1.position;
    		Vector2 direction2 = player.position - shotPoint2.position;

        	float angle1 = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;
        	float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;

        	Quaternion rotation1 = Quaternion.AngleAxis(angle1 - 90, Vector3.forward);
        	Quaternion rotation2 = Quaternion.AngleAxis(angle2 - 90, Vector3.forward);

        	shotPoint1.rotation = rotation1;
        	shotPoint2.rotation = rotation2;

        	Instantiate(enemyBullet, shotPoint1.position, shotPoint1.rotation);
        	Instantiate(enemyBullet, shotPoint2.position, shotPoint2.rotation);
        	
    	}
   }


   public void TakeDamage(int damageAmount){
   		health -= damageAmount;
   		healthBar.value = health;

      if(health <= halfHealth)
      {
        anim.SetTrigger("Stage2");

        
        
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


    public void Summon()
   {
   		if(player != null)
   		{
   			Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];	
   			Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
   		}
   }


    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		collision.GetComponent<Player>().TakeDamage(damage);
    	}
    }

}
