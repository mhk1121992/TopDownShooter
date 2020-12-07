using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet3 : MonoBehaviour
{
     public GameObject projectile;
    public GameObject muzzleFlash;
    
    public Transform shotPoint1;
    public Transform shotPoint2;
    public Transform shotPoint3;

    public Transform flashPoint;

    public float timeBetweenShots;
    

    private float shotTime;


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if(Input.GetMouseButton(0)){
        	if(Time.time >= shotTime)
        	{
        		Instantiate(muzzleFlash, flashPoint.position, transform.rotation);
        		Instantiate(projectile, shotPoint1.position, transform.rotation);
        		Instantiate(projectile, shotPoint2.position, transform.rotation);
        		Instantiate(projectile, shotPoint3.position, transform.rotation);
        		shotTime = Time.time + timeBetweenShots;
        	}
        }
    }
}
