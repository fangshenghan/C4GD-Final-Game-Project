using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //Health health;

    
    
    

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

    }

    public void attack(int num){
        health -= num;
    }
}
