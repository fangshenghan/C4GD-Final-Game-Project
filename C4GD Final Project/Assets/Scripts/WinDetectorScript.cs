using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetectorScript : MonoBehaviour
{

    public GameObject puzzleExitDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("DynamicParticle")){
            DynamicParticle dp = collision.gameObject.GetComponent<DynamicParticle>();
            if(dp.currentState == DynamicParticle.STATES.WATER){
                puzzleExitDoor.SetActive(false);
            }
        }
    }
}
