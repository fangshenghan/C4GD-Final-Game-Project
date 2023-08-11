using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeMain : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    private HealthUI healthUI;
    private PlayerInput playerInput;
    
    void Start()
    {
        healthUI = player.GetComponent<HealthUI>();
        playerInput = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("bulletDetected");
        if(other.CompareTag("bullet")){
            Destroy(other.gameObject);
            animator.SetBool("dead", true);
            StartCoroutine(DestroyF());
            
        }
        if(other.CompareTag("player")){
            playerInput.MovePlayerTowardsEnemy(transform);
            healthUI.attack(1);
        }
    }

    IEnumerator DestroyF(){
        
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }


}
