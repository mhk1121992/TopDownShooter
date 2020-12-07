using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
   

   private Vector2 targetPosition;
   

   public float timeBetweenSummons;
   private float summonTime;

   public Enemy enemyToSummon;
   

   private Animator anim;
   private bool facingRight;

   // public Transform shotPoint;

   // public GameObject enemyBullet;

  
   public float stopDistance;

   // private float attackTime;


   public override void Start()
   {
   		base.Start();
   		float randomX = Random.Range(minX, maxX);
   		float randomY = Random.Range(minY, maxY);
   		targetPosition = new Vector2(randomX, randomY);
		anim = GetComponent<Animator>();
   		
   }

   private void Update()
   {
   		if(player != null) {

        	
		   		
		   			//enemy will follow the player till it reaches the stopDistance (e.g. 1 unit)
			   		if(Vector2.Distance(transform.position, player.position) > stopDistance)
		        {
		        	//enemy moves toward player
		        	transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
					anim.SetBool("isRunning", true);


					if(player.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
						Flip();
					if(player.position.x < transform.position.x && facingRight)
						Flip();
		        }
		        else {
					anim.SetBool("isRunning", false);

		        	if(Time.time >= summonTime)
			   		{
			   			summonTime = Time.time + timeBetweenSummons;
						anim.SetTrigger("summon");
			   			
			   			
			   		}
		      //   	if(Time.time >= attackTime)
    				// {
		    		// 	attackTime = Time.time + timeBetweenAttacks;
		    		// 	RangedAttack();
    				// }

		        }
		   		
		   	

		   	
		   

	        
        	
        }

   }


   public void Summon()
   {
   		if(player != null)
   		{
   			Instantiate(enemyToSummon, transform.position, transform.rotation);
   		}
   }


   void Flip(){
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
		facingRight = !facingRight;
	}

    //  public void RangedAttack()
    // {
    // 	Vector2 direction = player.position - shotPoint.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    //     shotPoint.rotation = rotation;

    //     Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    // }
}
