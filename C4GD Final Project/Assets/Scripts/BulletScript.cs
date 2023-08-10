using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletScript : MonoBehaviour
{
    public Tilemap oceanMap;
    public bool coldBullet;
    public bool hotBullet;
    public GameObject platformRed;
    public GameObject platformBlue;

    private Vector3 platformTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int oceanLayerHealth = 3;
    public TileBase oceanHealth2, oceanHealth1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("DynamicParticle"))
        {
            Destroy(gameObject);
            DynamicParticle dp = collision.gameObject.GetComponent<DynamicParticle>();
            if (dp.currentState == DynamicParticle.STATES.WATER)
            {
                List<DynamicParticle> particles = ParticleHelper.findAllAdjacentParticles(dp, 10000);
                ParticleHelper.changeParticlesToState(particles, DynamicParticle.STATES.GAS, 50L);
            }else if (dp.currentState == DynamicParticle.STATES.GAS)
            {
                List<DynamicParticle> particles = ParticleHelper.findAllAdjacentParticles(dp, 10000);
                ParticleHelper.changeParticlesToState(particles, DynamicParticle.STATES.WATER, 50L);
            }
        }
        else if (other.CompareTag("OceanCollider"))
        {
            int y = (int)other.transform.position.y;
            for (int x = -10; x <= 40; x++)
            {
                if (oceanMap.HasTile(new Vector3Int(x, (y / 4) - 1)))
                {
                    oceanMap.SetTile(new Vector3Int(x, (y / 4) - 1), null);
                }
            }
            Vector3 pos = other.transform.position;
            pos.y -= 4;
            other.transform.position = pos;
        }
        if(other.CompareTag("enemyIceBullet") && coldBullet){
            Debug.Log("cold");
            platformTransform = other.transform.position;
            Instantiate(platformBlue, platformTransform, platformBlue.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        if(other.CompareTag("enemyFireBullet") && hotBullet){
            Debug.Log("hot");
            platformTransform = other.transform.position;
            Instantiate(platformRed, platformTransform, platformRed.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.CompareTag("platformBlue") && hotBullet){
            Destroy(other.gameObject);
        }
        if(other.CompareTag("platformRed") && coldBullet){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

}
