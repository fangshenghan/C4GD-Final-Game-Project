using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
    public float smallBound;
    public float bigBound;
    public bool horizontal;
    public bool vertical;
    public GameObject tile;
    private bool wait;
    private bool moveLeft;
    private bool activatedWait;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(horizontal && !wait && !moveLeft){
            tile.transform.Translate(Vector3.right * Time.deltaTime * 5);
        }
        if(horizontal && !wait && moveLeft){
            tile.transform.Translate(Vector3.left * Time.deltaTime * 5);
        }

        if(vertical && !wait && !moveLeft){
            tile.transform.Translate(Vector3.up * Time.deltaTime * 5);
        }
        if(vertical && !wait && moveLeft){
            tile.transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        
        


        
        if(horizontal && transform.position.x < smallBound && moveLeft){
            transform.position = new Vector3(smallBound, transform.position.y, transform.position.z);
            wait = true;
            //moveLeft = false;
        }
        if(horizontal && transform.position.x > bigBound && !moveLeft){
            transform.position = new Vector3(bigBound, transform.position.y, transform.position.z);
            wait = true;
            //moveLeft = true;
        }

        if(vertical && transform.position.y < smallBound && moveLeft){
            transform.position = new Vector3(transform.position.x, smallBound, transform.position.z);
            wait = true;
        }
        if(vertical && transform.position.y > bigBound && !moveLeft){
            transform.position = new Vector3(transform.position.x, bigBound, transform.position.z);
            wait = true;
        }

        if(wait && !activatedWait){
            activatedWait = true;
            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait(){
        yield return new WaitForSeconds(1.5f);
        wait = false;
        if(moveLeft){
            moveLeft = false;
        }
        else{
            moveLeft = true;
        }
        activatedWait = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.transform.SetParent(this.transform);
    }

    
    private void OnCollisionExit2D(Collision2D other) {
        other.transform.SetParent(null);
    }
}
