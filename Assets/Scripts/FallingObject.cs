using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    public float gravityScale = 1;

    private Rigidbody2D rb;
    private Vector2 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        EventManager.StartListening(Constants.RESTART_GAME, Reset);
    }

    public void TriggerFall()
    {
        rb.gravityScale = gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) 
        {
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }

    public void Reset(Hashtable h)
    {
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0.0f;
        rb.rotation = 0.0f;
        rb.gravityScale = 0;
        transform.position = startPosition;
    }
}
