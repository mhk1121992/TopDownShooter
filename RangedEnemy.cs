using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy 
{
     public float stopDistance;

     public float playerDistance;

    private float attackTime;
    public float timeBetweenAttacks;

    private Vector2 targetPosition;

    public Transform shotPoint;

    public GameObject enemyBullet;

    public override void Start()
   {
   		base.Start();
   		float randomX = Random.Range(minX, maxX);
   		float randomY = Random.Range(minY, maxY);
   		targetPosition = new Vector2(randomX, randomY);
   		
   }



    void Update()
    {
    	
    	// If the Player enters enemy zone the enemy starts moving towards the player, when player gets out of the zone, the enemy moves to a random position

    	// If the player is not null means not dead or destroyed
        if(player != null) {

        	//checking the distance between enemy and player
        	if(Vector2.Distance(transform.position, player.position) <= playerDistance)
		   	{
		   		
		   			//enemy will follow the player till it reaches the stopDistance (e.g. 1 unit)
			   		if(Vector2.Distance(transform.position, player.position) > stopDistance)
		        {
		        	//enemy moves toward player
		        	transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		        }
		        else {
		        	if(Time.time >= attackTime)
		        	{
		        		//enemy attacks the player
		        		
		        		attackTime = Time.time + timeBetweenAttacks;
		        		RangedAttack();
		        	}
		        }
		   		
		   	}

		else{

		   	
		    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		}
		   

	        
        	
        }

    void RangedAttack()
    {
    	Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}

}
