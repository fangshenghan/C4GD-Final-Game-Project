using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEvent : MonoBehaviour
{
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] float timeBetweenBullets = 0.2f;

    Vector2 lookDirection;
    float lookAngle;
    bool rifled, canShoot;

    // Update is called once per frame
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

/*
        if (gun2.activeSelf){
            rifled = true;
        }
*/

        if (Input.GetMouseButtonDown(0) && !rifled)
        {
            //Pistol Bullet
            //StartCoroutine(ShootBullets());
            FireNormalBullet();
        }
        else if (Input.GetMouseButtonDown(0) && rifled)
        {
            //Rifle Bullet
            FireRifleBullet();
        }
    }

    void FireRifleBullet()
    {
         StartCoroutine(ShootBullets());
    }
    
    void FireNormalBullet()
    {
        GameObject fireBullet = Instantiate(bullet, gunTip.position, gunTip.rotation);
        fireBullet.GetComponent<Rigidbody2D>().velocity = gunTip.up * bulletSpeed;
        Destroy(fireBullet, 10);
        //StartCoroutine(waitBetwnShots());
        //FireBullet();
    }


   

    IEnumerator ShootBullets()
    {
        //canShoot = false;

        for (int i = 0; i < 3; i++)
        {
            
            GameObject rifleBullet = Instantiate(bullet, gunTip.position, gunTip.rotation);
            rifleBullet.GetComponent<Rigidbody2D>().velocity = gunTip.up * bulletSpeed;
            yield return new WaitForSeconds(timeBetweenBullets);
        }

        //yield return new WaitForSeconds(0.1f);
        //canShoot = true;
    }
}