using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetect : MonoBehaviour
{
    public GameObject Player;
    Animator animator;
    PlayerInput playerInput;
    void Start()
    {
        animator = Player.GetComponent<Animator>();
        playerInput = Player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        animator.SetBool("inAir", false);
        playerInput.OnGround();
    }
}
