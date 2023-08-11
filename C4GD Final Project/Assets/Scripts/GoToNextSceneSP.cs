using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneSP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene2());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Scene2()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}