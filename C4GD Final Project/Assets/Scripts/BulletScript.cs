using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletScript : MonoBehaviour
{
    private Tilemap oceanMap;

    // Start is called before the first frame update
    void Start()
    {
        oceanMap = GameObject.Find("Ocean Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }

}
