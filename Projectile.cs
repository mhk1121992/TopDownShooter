using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public GameObject explosion;


    public int damage;

    public GameObject ShootingSound;

    void Start() 
    {
    	// Destroy(gameObject, lifeTime);
    	Invoke("DestroyProjectile", lifeTime);
        Instantiate(ShootingSound, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile(){
    	Destroy(gameObject);
    	Instantiate(explosion, transform.position, Quaternion.identity);
    	
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Enemy")
    	{
    		collision.GetComponent<Enemy>().TakeDamage(damage);
    		DestroyProjectile();
    	}

        if(collision.tag == "Boss")
        {
            

            collision.GetComponent<LanternBoss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }


    

    
    
}
