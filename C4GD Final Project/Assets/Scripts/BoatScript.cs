using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatScript : MonoBehaviour
{

    public static BoatScript instance;

    public static bool disableMovement = false;
    public Image image;
    public GameObject boatNotice, boat, imageObject, oceanCollider, oceanMap2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        imageObject.SetActive(true);
    }

    private float targetAlpha = 0F;
    private float boatXTarget = -35F;

    // Update is called once per frame
    void Update()
    {
        if(image.color.a == 0F){
            image.gameObject.SetActive(false);
        }else{
            image.gameObject.SetActive(true);
        }
        if(image.color.a < targetAlpha){
            image.color = new Color(0, 0, 0, Mathf.Min(image.color.a + Time.deltaTime, 1F));
        }else if(image.color.a > targetAlpha){
            image.color = new Color(0, 0, 0, Mathf.Max(image.color.a - Time.deltaTime, 0F));
        }
        if(boat.transform.position.x < boatXTarget){
            boat.transform.Translate(Vector3.right * 20 * Time.deltaTime);
        }else if(boat.transform.position.x > boatXTarget){
            boat.transform.Translate(Vector3.left * 20 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag("Boat")){
            return;
        }
        if(!disableMovement){
            if(oceanCollider.transform.position.y == -10f || oceanMap2.active){
                disableMovement = true;
                boatNotice.SetActive(true);
            }
        }
    }


    public int side = 1;
    public void sailToOtherSide(){
        boatNotice.SetActive(false);
        targetAlpha = 1F;
        StartCoroutine(SailStep1());
        GetComponent<Animator>().SetBool("noMove", true);
    }

    public void cancelSail(){
        disableMovement = false;
        boatNotice.SetActive(false);
    }

    IEnumerator SailStep1(){
        yield return new WaitForSeconds(2);
        targetAlpha = 0F;
        transform.parent = boat.transform;
        transform.localPosition = new Vector3(0, 1, 0);
        if(boatXTarget > 100){
            boatXTarget = 131;
            boat.transform.position = new Vector3(131, -7f, 0);
        }else{
            boatXTarget = 4;
            boat.transform.position = new Vector3(4, -7f, 0);
        }
        yield return new WaitForSeconds(2);
        if(boatXTarget > 100){
            boatXTarget = 6.5F;
            yield return new WaitForSeconds(3.5f);
            targetAlpha = 1F;
            yield return new WaitForSeconds(4);
            targetAlpha = 0F;
            transform.parent = null;
            boatXTarget = -35f;
            transform.position = new Vector3(-48, 2, 0);
            boat.transform.position = new Vector3(-35f, -3.4f, 0);
            yield return new WaitForSeconds(1);
            disableMovement = false;
            GetComponent<Animator>().SetBool("noMove", false);
        }else{
            boatXTarget = 132.5F;
            yield return new WaitForSeconds(3.5f);
            targetAlpha = 1F;
            yield return new WaitForSeconds(4);
            targetAlpha = 0F;
            transform.parent = null;
            boatXTarget = 152.8f;
            boat.transform.position = new Vector3(152.8f, -7.1f, 0);
            transform.position = new Vector3(168, -2.5f, 0);
            yield return new WaitForSeconds(1);
            disableMovement = false;
            GetComponent<Animator>().SetBool("noMove", false);
        }
        
    }

    public void setTargetAlpha(float alpha){
        targetAlpha = alpha;
    }


}
