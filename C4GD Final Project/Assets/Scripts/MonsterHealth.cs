using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int health;
    public bool hot;
    public bool cold;
    public GameObject heart;
    public Transform playerTransform;
    public ParticleSystem explosion;
    private SpriteRenderer mySprite;
    private bool played;
    private bool dead;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(health <= 0 && !played){
            played = true;
            dead = true;
            explosion.Play();
            mySprite.enabled = false;
            StartCoroutine(deathDelay());
            Vector3 heartPosition = new Vector3(0, 8, 0) + playerTransform.position;
            Instantiate(heart, heartPosition, gameObject.transform.rotation);
            
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("coldBullet") && hot){
            health--;
        }
        if(other.gameObject.CompareTag("hotBullet") && cold){
            health--;
        }
    }

    IEnumerator deathDelay(){

        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

    public bool getIfDead(){
        return dead;
    }
}
