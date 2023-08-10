using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    private GameObject door;
    private SpriteRenderer sr;
    private bool isLeverOn = false;

    private float doorYTarget = 0F, doorXTarget = 0F;
    private float doorMoveSpeed = 10F;

    public float doorOpenY, doorOpenX;
    public Sprite lever_on, lever_off;

    // Start is called before the first frame update
    void Start()
    {
        door = transform.parent.gameObject.transform.Find("DoorTiles").gameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 doorPos = door.transform.localPosition;
        if (doorPos.y == doorYTarget && doorPos.x == doorXTarget)
        {
            return;
        }
        float deltaY = doorPos.y - doorYTarget, deltaX = doorPos.x - doorXTarget;
        if (Mathf.Abs(deltaY) < 0.1F)
        {
            doorPos.y = doorYTarget;
        }
        else if (deltaY > 0)
        {
            doorPos.y -= Time.deltaTime * doorMoveSpeed;
        }
        else if (deltaY < 0)
        {
            doorPos.y += Time.deltaTime * doorMoveSpeed;
        }
        if (Mathf.Abs(deltaX) < 0.1F)
        {
            doorPos.x = doorXTarget;
        }
        else if (deltaX > 0)
        {
            doorPos.x -= Time.deltaTime * doorMoveSpeed;
        }
        else if (deltaX < 0)
        {
            doorPos.x += Time.deltaTime * doorMoveSpeed;
        }
        door.transform.localPosition = doorPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (isLeverOn)
            {
                sr.sprite = lever_off;
                doorYTarget = 0F;
                doorXTarget = 0F;
            }
            else
            {
                sr.sprite = lever_on;
                doorXTarget = doorOpenX;
                doorYTarget = doorOpenY;
            }
            isLeverOn = !isLeverOn;
        }
    }
}
