using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneGP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Scene1()
    {
        yield return new WaitForSeconds(4.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
