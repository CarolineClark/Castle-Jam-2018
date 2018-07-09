using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {
    public float gravityScale = 1;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TriggerFall()
    {
        rb.gravityScale = gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) 
        {
            Debug.Log("player hit");
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }
}
