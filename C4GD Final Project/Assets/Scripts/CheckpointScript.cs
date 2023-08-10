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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            spawnPos = player.transform.position;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
