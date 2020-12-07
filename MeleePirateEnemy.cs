using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePirateEnemy : Enemy
{
    public float stopDistance;

    

	private float attackTime;
	public float timeBetweenAttacks;

	private Vector2 targetPosition;
	private bool facingRight;


	private Animator anim;
   
	public override void Start()
   {
   		base.Start();
   		float randomX = Random.Range(minX, maxX);
   		float randomY = Random.Range(minY, maxY);
   		targetPosition = new Vector2(randomX, randomY);
		anim = GetComponent<Animator>();
   		
   }
 

    // Update is called once per frame
    void Update()
    {
    	
    	// If the Player enters enemy zone the enemy starts moving towards the player, when player gets out of the zone, the enemy moves to a random position

    	// If the player is not null means not dead or destroyed
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

		        	if(Time.time >= attackTime)
		        	{
		        		//enemy attacks the player
		        		StartCoroutine(Attack());
						anim.SetTrigger("attack");
						
		        		attackTime = Time.time + timeBetweenAttacks;
		        	}
		        }
		   		
		   	

		   	
		   

	        
        	
        }

        //attack function
        IEnumerator Attack()
        {
        	player.GetComponent<Player>().TakeDamage(damage);


        	// Vector2 originalPosition = transform.position;
        	// Vector2 targetPosition = player.position;

        	// float percent = 0;

        	// while(percent <= 1)
        	// {
			// 	anim.SetTrigger("attack");
        	// 	percent += Time.deltaTime * attackSpeed;
        	// 	float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
        	// 	transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
        		yield return null;
        	// }
        }


    }


	void Flip(){
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
		facingRight = !facingRight;
	}
}
