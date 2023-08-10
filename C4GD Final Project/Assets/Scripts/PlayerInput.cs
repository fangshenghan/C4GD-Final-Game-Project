using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] float playerMoveSpeed = 8f;
    //[SerializeField] float playerJumpSpeed = 12f;
    [SerializeField] float moveSpeed = 50f;
    //[SerializeField] GameObject bossPoint_1;

    //public cameraSwitcher cameraSwitcher;
    

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Transform enemyTransform;
    private bool isMoving;
    private bool learned;
    private bool fakeGunActivate;
    
    private bool unlimitJump;
    //bool boss1;
    bool level3Entered;
    //public GameObject bullet;
    bool facingRight = true;
    Vector2 delta;
    Rigidbody2D myRigidbody;
    //public Transform bulletSpawnPoint;
    bool isJumping;
    bool isFiring = false;
    bool hasEatenBonus = false;
    bool fly;
    Vector2 moveInput;

    Vector3 initialLocalScale;
    public GameObject myGun;
    //public GameObject coldGun;
    //public GameObject hotGun;
    public bool notInBoss1Area = true;
    public bool notInBoss2Area = true;
    public bool notInLevel3 = true;
    public bool notInFinalBoss = true;
    public TextMeshProUGUI interactHintText;
    bool inDefault = true;

    bool inAir;

    public float playerJumpSpeed = 10f;
    private bool canJump = true;
    private BoxCollider2D feetCollider;

    public Animator animator;
    private bool canMouseFlip = true;

    public GameObject VMCam;
    private MyCameraSwitcher myCameraSwitcher;
    private HealthUI healthUI;
    

    void Start() 
    {
        healthUI = GetComponent<HealthUI>();
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        initialLocalScale = transform.localScale;
        myCameraSwitcher = VMCam.GetComponent<MyCameraSwitcher>();


        //feetCollider = transform.Find("FeetCollider").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //fly = true;
        Move();
        
        animator.SetFloat("speed", Math.Abs(moveInput.x * playerMoveSpeed));

        if(myRigidbody.velocity.x == 0f && myRigidbody.velocity.y == 0f){
            /*
            if(hotGun.active){
                hotGun.SetActive(false);
            }
            else{
                coldGun.SetActive(false);
            }
            */
            myGun.SetActive(true);
            canMouseFlip = true;
            
        }
        else{
            myGun.SetActive(false);
            canMouseFlip = false;
        }
      

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(mousePos.x > transform.position.x && !facingRight && !inAir && canMouseFlip) // flip right
        {
            flip();
        }
        else if (mousePos.x < transform.position.x && facingRight && !inAir && canMouseFlip) // flip left
        {
            flip();
        }

        
        
        if (isMoving && enemyTransform != null)
        {
            // Calculate the target position based on the enemy's position
            Vector3 targetPosition = new Vector3(enemyTransform.position.x + (transform.position.x < enemyTransform.position.x ? -8f : 8f), transform.position.y, transform.position.z);

            // Move the player towards the target position at the specified speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15 * Time.deltaTime);

            // Check if the player has reached the target position
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }




            
        if (isMoving && enemyTransform != null)
        {
            // Calculate the target position based on the enemy's position
            Vector3 targetPosition = new Vector3(enemyTransform.position.x + (transform.position.x < enemyTransform.position.x ? -8f : 8f), transform.position.y, transform.position.z);

            // Move the player towards the target position at the specified speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15 * Time.deltaTime);

            // Check if the player has reached the target position
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    
    
    

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    
    public void MovePlayerTowardsEnemy(Transform enemyTransform)
    {
        this.enemyTransform = enemyTransform;
        isMoving = true;
    }
    

    

    /*void OnFire(InputValue value)
    {
        if(value.isPressed)
        {
            isFiring = true;
            if(!hasEatenBonus)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f,0f,90f));
            }
            else
            {
                for(int i = 0; i<4; i++)
                Instantiate(bullet, transform.position, Quaternion.Euler(0f,0f,i * 90f));
            }
            //bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
            //Rigidbody2D bulletRigidbody = bullet.AddComponent<Rigidbody2D>();
            //bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
        }
    }*/
    
    /*
    void Move()
    {
        updateFlip();
        //Vector3 currentPosition = transform.position;
        //currentPosition.x += delta.x;
        //transform.position = currentPosition;
        
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

    

        if(isJumping)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerJumpSpeed);
            isJumping = false;
        }

    }
    */

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("redMonster1")){
            myCameraSwitcher.ChangeToRedMonster1();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("enemyFireBullet")){
            healthUI.attack(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("enemyIceBullet")){
            healthUI.attack(1);
            Destroy(other.gameObject);
        }
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && !inAir)
        {
            animator.SetBool("inAir", true);
            isJumping = true;
            inAir = true;
        }
    }

    void Move()
    {
        float moveX = moveInput.x;
        
        
        if (moveX != 0)
        {
            // Player is moving horizontally
            Vector2 playerVelocity = new Vector2(moveX * playerMoveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            if(!canMouseFlip && facingRight && moveX < 0){
                flip();
            }
            if(!canMouseFlip && !facingRight && moveX > 0){
                flip();
            }
        
        }
        else
        {
            // Player is not moving horizontally, retain the current velocity and sprite scale
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }

        if (canJump && Input.GetButtonDown("Jump") && !fly)
        {
            //Jump();
        }
        
        if (isJumping)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerJumpSpeed);
            isJumping = false;
        }
        
        
    }
    
    
    private void Jump()
    {
        if(!unlimitJump){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerJumpSpeed);
            canJump = false;
        }
    }
        
    

   

    private IEnumerator ColorChangeCoroutine(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }


    void flip(){
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

    public void OnGround(){
        inAir = false;
    }

    

}

