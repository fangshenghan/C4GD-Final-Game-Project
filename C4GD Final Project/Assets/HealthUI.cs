using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject guns, deathtext;

    public Animator animator;
    public bool isDead = false;

    //Health health;

    
    void start(){
        animator = gameObject.GetComponent<Animator>();
    }
    

    void Update(){
        
        //health = this.GetComponent<Health>();

        for (int i = 0; i < hearts.Length;i++){
            if(health > numOfHearts){
                health = numOfHearts;
            }

            if(i < health){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts){
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
        if(health <= 0 && !isDead){
            isDead = true;
            animator.SetBool("dead", true);
            guns.SetActive(false);
            BoatScript.instance.setTargetAlpha(1);
            deathtext.SetActive(true);
            StartCoroutine(respawn());
        }

    }

    public void attack(int num){
        health -= num;
    }

    IEnumerator respawn(){
        yield return new WaitForSeconds(1.5F);
        SceneManager.LoadScene("EthanDev 1");
    }
}
