using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetectorScript : MonoBehaviour
{
    public GameObject player;
    public GameObject clouds, oceanMap1, oceanMap2;

    // Start is called before the first frame update
    void Start()
    {
        if(StaticVars.isBossDead){
            clouds.SetActive(true);
            oceanMap1.SetActive(false);
            oceanMap2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("player")){
            BoatScript.instance.setTargetAlpha(1F);
            StartCoroutine(tp());
            StartCoroutine(PlayerInput.instance.reduceVolume2());
        }
    }

    IEnumerator tp(){
        yield return new WaitForSeconds(1.5F);
        player.transform.position = new Vector3(-80, 1.8F);
        BoatScript.instance.setTargetAlpha(0F);
        StaticVars.spawnPos = new Vector3(-80, 1.8F);
        StaticVars.isBossDead = true;
        clouds.SetActive(true);
        PlayerInput.instance.audioForDown1.volume = 1;
        PlayerInput.instance.audioUp.SetActive(true);
        oceanMap1.SetActive(false);
    }
}
