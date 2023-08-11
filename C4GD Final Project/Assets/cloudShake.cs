using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class cloudShake : MonoBehaviour
{
    // Start is called before the first frame update
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.5f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;
    public RainScript2D rainScript2D;
    public GameObject oceanMap2;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coldBullet"))
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
            }
            
            shakeCoroutine = StartCoroutine(Shake());
            if(StaticVars.isBossDead){
                rainScript2D.IncreaseIntensity();
                if(rainScript2D.RainIntensity >= 0.9f && !oceanMap2.active){
                    BoatScript.instance.setTargetAlpha(1);
                    StartCoroutine(FillOcean());
                }
            }
        }
    }
    
    IEnumerator FillOcean()
    {
        yield return new WaitForSeconds(1.5F);
        Destroy(GameObject.Find("Ocean Tilemap"));
        oceanMap2.SetActive(true);
        BoatScript.instance.setTargetAlpha(0);
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitCircle * shakeIntensity;
            transform.position = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
