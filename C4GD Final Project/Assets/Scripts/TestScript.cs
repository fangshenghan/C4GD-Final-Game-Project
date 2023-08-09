using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static DynamicParticle;

public class TestScript : MonoBehaviour
{
    public Tilemap tm;

    // Start is called before the first frame update
    void Start()
    {

    }


    private float x = -0.5f, y = -2.6f;
    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0;i < 2;i++)
        {
            if (x < 9f)
            {
                if (y < -1.5f)
                {
                    GameObject newLiquidParticle = (GameObject)Instantiate(Resources.Load("LiquidPhysics/DynamicParticle"), new Vector3(x, y, 0), transform.rotation);
                    DynamicParticle particleScript = newLiquidParticle.GetComponent<DynamicParticle>();
                    particleScript.SetLifeTime(600);
                    particleScript.SetState(STATES.WATER);
                    newLiquidParticle.transform.parent = GameObject.Find("LiquidParticles").transform;
                    y += 0.2f;
                }
                else
                {
                    x += 0.2f;
                    y = -2.6f;
                }
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (DynamicParticle dp in FindObjectsOfType<DynamicParticle>())
            {
                dp.SetState(DynamicParticle.STATES.GAS);
            }
        }
    }
}
