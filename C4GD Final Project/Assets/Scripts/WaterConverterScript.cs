using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterConverterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("DynamicParticle"))
        {
            return;
        }
        DynamicParticle dp = collision.gameObject.GetComponent<DynamicParticle>();
        if (dp.currentState == DynamicParticle.STATES.GAS)
        {
            ParticleHelper.changeParticlesToState(ParticleHelper.findAllAdjacentParticles(dp, 8), DynamicParticle.STATES.WATER, 20L);
        }
    }
}
