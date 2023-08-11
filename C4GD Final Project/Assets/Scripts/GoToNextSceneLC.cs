using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneLC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene3());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Scene3()
    {
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("EthanDev 1");
    }
}
