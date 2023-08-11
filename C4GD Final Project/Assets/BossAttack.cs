using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject projectilePrefab;
    public float shootInterval = 0.2f;
    public float projectileSpeed;
    bool recentFired, boss_1;
    public GameObject finalDoor;
    public audioPlayer audioPlayer;
    //PlayerInputScript playerInputScript;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 currentPlayerPosition;
    private PlayerInput playerInputReference;
    private int health = 25;
    //private float nextShootTime;
    //private float timeSinceLastShot;
    [SerializeField] GameObject player;
    private PlayerInput playerInput;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerInput = player.GetComponent<PlayerInput>();
        //timeSinceLastShot = shootInterval;
    }

    void Update()
    {
        
        if(playerInput.GetIfEnterBossArea())
        {
            if (!recentFired)
            {
                StartCoroutine(PlaysAIShootCoroutine());
            }
        }

        if(health <= 0){
            gameObject.SetActive(false);
            finalDoor.SetActive(false);
        }
    }

    private IEnumerator PlaysAIShootCoroutine()
    {
        // Calculate the distance between the player and monster
        
        float randomYAxisPosition = Random.Range(0f, 1f) < 0.2f ? -112.4f : -124.3f;
        Shoot(randomYAxisPosition);
        recentFired = true;
        yield return new WaitForSeconds(shootInterval);
        recentFired = false;
        
    }


     void Shoot(float yPosition)
    {
        // Instantiate the projectile at the enemy's position with the y-axis offset
        Vector3 firePosition = new Vector3(transform.position.x, yPosition, transform.position.z);
        GameObject projectile = Instantiate(projectilePrefab, firePosition, Quaternion.identity);
        // Calculate the direction to shoot
        Vector3 shootDirection = Vector3.right;
        // Set the speed of the projectile
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
        // Add any additional logic or effects for the projectile
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("coldBullet")){
            StartCoroutine(ColorChangeCoroutineBlue());
            health--;
            audioPlayer.PlayBossHurtAudio();
        }
        if(other.gameObject.CompareTag("hotBullet")){
            StartCoroutine(ColorChangeCoroutineRed());
            health--;
            audioPlayer.PlayBossHurtAudio();
        }
    }

    private IEnumerator ColorChangeCoroutineRed()
    {
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator ColorChangeCoroutineBlue()
    {
        
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
