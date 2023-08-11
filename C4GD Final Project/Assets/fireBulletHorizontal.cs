using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBulletHorizontal : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float horizontalDistance;
    public float verticalThreshold;
    public Transform playerTransform;
    public float shootInterval;
    public float projectileSpeed;

    private bool inRange;
    private Vector3 currentPlayerPosition;
    private bool facingRight;
    private float timeSinceLastShot;
    private Vector3 initialLocalScale;
    private MonsterHealth monsterHealth;

    void Start()
    {
        monsterHealth = GetComponent<MonsterHealth>();
        timeSinceLastShot = shootInterval;
        initialLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the player and monster
        float verticalDistance = Mathf.Abs(playerTransform.position.y - transform.position.y);
        float horizontalDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);

        // Check if player is within the specified thresholds
        bool withinVerticalThreshold = verticalDistance <= verticalThreshold;
        bool withinHorizontalDistance = horizontalDistance <= this.horizontalDistance;

        if (withinVerticalThreshold && withinHorizontalDistance)
        {
            inRange = true;
            // Shoot at player's direction
            currentPlayerPosition = playerTransform.position;
            if(currentPlayerPosition.x < transform.position.x && facingRight){
                flip();
            }
            else if (currentPlayerPosition.x > transform.position.x && !facingRight){
                flip();
            }
            if (Time.time >= timeSinceLastShot && !monsterHealth.getIfDead())
            {
                ShootAtPlayer();
                timeSinceLastShot = Time.time + shootInterval;
            }
        }
        else{
            inRange = false;
        }
    }

    void flip()
    {
        if(transform.localScale.x > 0)
        {
            Vector3 newScale = new Vector3(-initialLocalScale.x, initialLocalScale.y,initialLocalScale.z);
            transform.localScale = newScale;
            facingRight = false;
        }else if(transform.localScale.x < 0){
            Vector3 newScale = new Vector3(initialLocalScale.x, initialLocalScale.y,initialLocalScale.z);
            transform.localScale = newScale;
            facingRight = true;
        }
    }

    private void ShootAtPlayer()
    {
        Debug.Log("shoot");
        
        // Calculate the direction from the monster to the player
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        // Instantiate and shoot a projectile in the calculated direction
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.velocity = direction * projectileSpeed;
        
    }
}
