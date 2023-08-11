using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(!other.gameObject.CompareTag("boss") && !other.gameObject.CompareTag("coldBullet") || !other.gameObject.CompareTag("hotBullet")){
            Destroy(gameObject);
        }
        
        
    }
}
