using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static DynamicParticle;

public class WaterGeneratorScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("WaterSource"))
        {
            float deltaX = go.transform.localScale.x / 2;
            float deltaY = go.transform.localScale.y / 2;
            float startX = go.transform.position.x - deltaX + 0.05F;
            float startY = go.transform.position.y - deltaY + 0.05F;
            float endX = go.transform.position.x + deltaX - 0.05F;
            float endY = go.transform.position.y + deltaY - 0.05F;
            //Debug.Log(startX + " " + startY + " " + endX + " " + endY);
            for (;startX <= endX; startX += 0.025F)
            {
                for (; startY <= endY; startY += 0.025F)
                {
                    GameObject newLiquidParticle = (GameObject)Instantiate(Resources.Load("LiquidPhysics/DynamicParticle"), new Vector3(startX, startY, 0), transform.rotation);
                    DynamicParticle particleScript = newLiquidParticle.GetComponent<DynamicParticle>();
                    particleScript.SetLifeTime(1000000);
                    particleScript.SetState(STATES.WATER);
                    newLiquidParticle.transform.parent = GameObject.Find("LiquidParticles").transform;
                }
            }
            Destroy(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (DynamicParticle dp in FindObjectsOfType<DynamicParticle>())
            {
                dp.SetState(DynamicParticle.STATES.GAS);
            }
        }
    }
}
