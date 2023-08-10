using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;
public class GunEvent : MonoBehaviour
{
    [SerializeField] Transform gunTipHot;
    [SerializeField] Transform gunTipCold;
    [SerializeField] GameObject hotBullet;
    [SerializeField] GameObject coldBullet;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject coldGun;
    [SerializeField] GameObject hotGun;
    [SerializeField] float timeBetweenBullets = 0.2f;

    Vector2 lookDirection;
    float lookAngle;
    bool rifled, canShoot;
    public GameObject rain;
    public RainScript2D rainScript2D;

    void start(){
        rainScript2D = rain.GetComponent<RainScript2D>();
    }
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
        if(Input.GetKeyDown(KeyCode.E)){
            if(coldGun.active){
                coldGun.SetActive(false);
                hotGun.SetActive(true);
            }
            else{
                coldGun.SetActive(true);
                hotGun.SetActive(false);
            }
        }

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
        GameObject fireBullet;
        if(hotGun.active){
            fireBullet = Instantiate(hotBullet, gunTipHot.position, gunTipHot.rotation);
        }
        else{
            fireBullet = Instantiate(coldBullet, gunTipCold.position, gunTipCold.rotation);
        }
        
        fireBullet.GetComponent<Rigidbody2D>().velocity = lookDirection.normalized * bulletSpeed;
        Destroy(fireBullet, 10);
        rainScript2D.IncreaseIntensity();
        //StartCoroutine(waitBetwnShots());
        //FireBullet();
    }


   

    IEnumerator ShootBullets()
    {
        //canShoot = false;

        for (int i = 0; i < 3; i++)
        {
            
            GameObject rifleBullet = Instantiate(hotBullet, gunTipHot.position, gunTipHot.rotation);
            rifleBullet.GetComponent<Rigidbody2D>().velocity = gunTipHot.up * bulletSpeed;
            yield return new WaitForSeconds(timeBetweenBullets);
        }

        //yield return new WaitForSeconds(0.1f);
        //canShoot = true;
    }
}