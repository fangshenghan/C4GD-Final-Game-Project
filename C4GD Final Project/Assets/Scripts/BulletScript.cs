using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletScript : MonoBehaviour
{
    public Tilemap oceanMap;

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
            oceanLayerHealth--;
            if (oceanLayerHealth == 2)
            {
                for (int x = -10; x <= 10; x++)
                {
                    if (oceanMap.HasTile(new Vector3Int(x, y - 1)))
                    {
                        oceanMap.SetTile(new Vector3Int(x, y - 1), oceanHealth2);
                    }
                }
            }
            else if (oceanLayerHealth == 1)
            {
                for (int x = -10; x <= 10; x++)
                {
                    if (oceanMap.HasTile(new Vector3Int(x, y - 1)))
                    {
                        oceanMap.SetTile(new Vector3Int(x, y - 1), oceanHealth1);
                    }
                }
            }
            else
            {
                for (int x = -10; x <= 10; x++)
                {
                    if (oceanMap.HasTile(new Vector3Int(x, y - 1)))
                    {
                        oceanMap.SetTile(new Vector3Int(x, y - 1), null);
                    }
                }
                oceanLayerHealth = 3;
                Vector3 pos = other.transform.position;
                pos.y--;
                other.transform.position = pos;
            }
        }
    }

}
