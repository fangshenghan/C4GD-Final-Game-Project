using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public static Vector3 spawnPos;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("checkpoint");
        if (collision.gameObject.CompareTag("player"))
        {
            spawnPos = player.transform.position;
        }
    }
}
