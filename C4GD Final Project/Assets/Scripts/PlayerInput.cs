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
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] float playerMoveSpeed = 8f;
    //[SerializeField] float playerJumpSpeed = 12f;
    [SerializeField] float moveSpeed = 50f;
    //[SerializeField] GameObject bossPoint_1;

    //public cameraSwitcher cameraSwitcher;
    public static PlayerInput instance;
    //public bool slowerRed;
    

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Transform enemyTransform;
    private bool isMoving;
    private bool learned;
    private bool fakeGunActivate;
    public AudioSource audioForDown1, audioForDown2;
    
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
    public GameObject audioUp;
    public GameObject audioDown;

    public GameObject gun1, gun2;
    bool inDefault = true;

    bool inAir;

    public float playerJumpSpeed = 10f;
    private bool canJump = true;
    private BoxCollider2D feetCollider;

    public Animator animator;
    private bool canMouseFlip = true;

    public GameObject VMCam, oceanMap2, oceanCollider, puzzleResetBtn, wizard;
    private MyCameraSwitcher myCameraSwitcher;
    private HealthUI healthUI;
    private bool finalBossEntered;


    private GunEvent gunEvent1;
    private GunEvent gunEvent2;


            
    void Start() 
    {
        gunEvent1 = gun1.GetComponent<GunEvent>();
        gunEvent2 = gun2.GetComponent<GunEvent>();
        instance = this;
        healthUI = GetComponent<HealthUI>();
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        myCameraSwitcher = VMCam.GetComponent<MyCameraSwitcher>();
        if(StaticVars.spawnPos.x == -10000){
            StaticVars.spawnPos = gameObject.transform.position;
        }
        gameObject.transform.position = StaticVars.spawnPos;
        if(StaticVars.isBossDead){
            oceanCollider.transform.position -= new Vector3(0, 20, 0);
        }
        if((StaticVars.isInPuzzle || StaticVars.isPuzzleDone) && !StaticVars.isBossDead){
            audioDown.SetActive(true);
            audioUp.SetActive(false);
        }
        audioForDown1 = audioUp.GetComponent<AudioSource>();
        audioForDown2 = audioDown.GetComponent<AudioSource>();
        //feetCollider = transform.Find("FeetCollider").GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        puzzleResetBtn.SetActive(StaticVars.isInPuzzle);
        if(BoatScript.disableMovement){
            return;
        }
        //fly = true;
        Move();
        
        animator.SetFloat("speed", Math.Abs(moveInput.x * playerMoveSpeed));

        if(myRigidbody.velocity.magnitude == 0f && !inAir){
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

    
    public void resetPuzzle(){
        SceneManager.LoadScene("EthanDev 1");
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
    

    


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("redMonster1")){
            myCameraSwitcher.ChangeToRedMonster1();
        }
        if(other.CompareTag("finalBossBoundry")){
            myCameraSwitcher.ChangeToFinalBossCam();
            finalBossEntered = true;
        }
        if(other.gameObject.CompareTag("heart")){
            healthUI.attack(-1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Checkpoint")){
            StaticVars.spawnPos = gameObject.transform.position;
            if(other.gameObject.name.Contains("CheckpointPuzzleIn")){
                StaticVars.isInPuzzle = true;
            }else if(other.gameObject.name.Contains("CheckpointPuzzleOut")){
                StaticVars.isInPuzzle = false;
                StaticVars.isPuzzleDone = true;
            }
            Debug.Log("checkpoint");
        }
        if(other.CompareTag("oceanBtnView")){
            myCameraSwitcher.ChangeToOceanBtnCam();
        }
        if(other.CompareTag("puzzleBoundry")){
            myCameraSwitcher.ChangeToPuzzleCam();
        }
        if(other.CompareTag("beforebossBoundry")){
            myCameraSwitcher.ChangeToBeforeBossCam();
        }
        if(other.CompareTag("toDown1")){
            StartCoroutine(reduceVolume());
        }
        if(other.CompareTag("toDown2")){
            audioDown.SetActive(true);
            audioUp.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("finalBossBoundry")){
            myCameraSwitcher.ChangeToDefaultCam();
        }
        if(other.CompareTag("oceanBtnView")){
            myCameraSwitcher.ChangeToDefaultCam();
        }
    }

    public GameObject at;

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("enemyFireBullet")){
            healthUI.attack(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("enemyIceBullet")){
            healthUI.attack(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("bullet")){
            healthUI.attack(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("spike")){
            healthUI.attack(100);
        }
        if(other.gameObject.CompareTag("platformBlue")){
            if(gameObject.transform.parent == null){
                gameObject.transform.SetParent(other.gameObject.transform);
            }
        }
        if(other.gameObject.CompareTag("platformRed")){
            if(gameObject.transform.parent == null){
                gameObject.transform.SetParent(other.gameObject.transform);
            }
        }
        if(other.gameObject.CompareTag("iceComponent")){
            gunEvent1.gotComponent();
            gunEvent2.gotComponent();
            Destroy(other.gameObject);
            wizard.GetComponent<npc>().setLearned(false);
            at.SetActive(true);
            StartCoroutine(aaa());
        }
        
    }

    IEnumerator aaa(){
        yield return new WaitForSeconds(5);
        at.SetActive(false);
    }

    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.CompareTag("platformBlue")){
            gameObject.transform.SetParent(null);
        }
        if(other.gameObject.CompareTag("platformRed")){
            gameObject.transform.SetParent(null);
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
    
    /*
    private void Jump()
    {
        if(!unlimitJump){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, playerJumpSpeed);
            canJump = false;
        }
    }
        */
    

   

    private IEnumerator ColorChangeCoroutine(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }


    void flip(){
        if(transform.localScale.x > 0)
        {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            facingRight = false;
        }else if(transform.localScale.x < 0){
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            facingRight = true;
        }
    }

    public void OnGround(){
        inAir = false;
    }

    public bool GetIfEnterBossArea(){
        return finalBossEntered;
    }

    public IEnumerator reduceVolume(){
        yield return new WaitForSeconds(0.24f);
        audioForDown1.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown1.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown1.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown1.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown1.volume -= 0.2f;
    }
    
    public IEnumerator reduceVolume2(){
        yield return new WaitForSeconds(0.24f);
        audioForDown2.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown2.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown2.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown2.volume -= 0.2f;
        yield return new WaitForSeconds(0.25f);
        audioForDown2.volume -= 0.2f;
    }
    

}

