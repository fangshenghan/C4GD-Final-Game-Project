using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeFlip : MonoBehaviour
{
    public Transform playerTransform;
    private bool faceRight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(faceRight && playerTransform.position.x < transform.position.x){
            flip();
        }
        if(!faceRight && playerTransform.position.x > transform.position.x){
            flip();
        }
        

    }


    private void flip(){
        
            Vector3 currentScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, currentScale.z);

        if(faceRight){
            faceRight = false;
        }
        else{
            faceRight = true;
        }
    }
}
