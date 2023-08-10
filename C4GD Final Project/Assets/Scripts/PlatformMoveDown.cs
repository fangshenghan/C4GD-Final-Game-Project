using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveDown : MonoBehaviour
{

    public float moveSpeed = 10F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
