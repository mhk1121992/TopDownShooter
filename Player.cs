using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private Vector2 moveAmount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Animator anim;

    Animator cameraAnim;
    public int health;

    public Animator hurtAnim;

    private SceneTransition sceneTransitions;


    void Start()
    {
    	
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cameraAnim = Camera.main.GetComponent<Animator>();

        //change scene when player dies
        sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);
        }
       
    }

    void FixedUpdate()
    {
    	rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }


    public void TakeDamage(int damageAmount){
   		health -= damageAmount;
   		cameraAnim.SetTrigger("shake");
   		hurtAnim.SetTrigger("hurt");
   		UpdateHealthUI(health);

   		if(health <= 0)
   		{
   			Destroy(gameObject);
   			sceneTransitions.LoadScene("Lose");
   		}
    }

    // change to two bullet weapon
    public void ChangeWeapon2(WeaponBullet2 weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    // change to three bullet weapon
    public void ChangeWeapon3(WeaponBullet3 weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHealthUI(int currentHealth){
    	for(int i = 0; i < hearts.Length; i++)
    	{
    		if(i < currentHealth)
    		{
    			hearts[i].sprite = fullHeart;
    		}
    		else
    		{
    			hearts[i].sprite = emptyHeart;
    		}
    	}
    }

    public void Heal(int healAmount)
    {
    	if(health + healAmount > 5)
    	{
    		health = 5;
    	}
    	else
    	{
    		health += healAmount;
    	}
    	UpdateHealthUI(health);
    }
}
