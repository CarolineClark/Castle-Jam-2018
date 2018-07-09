using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float jumpSpeed = 10f;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private int layerMask;

    void Start () 
    {
        layerMask = 1 << Constants.GROUND_LAYER;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        EventManager.StartListening(Constants.FALLING_OBJECT_HIT_EVENT, DeathByFallingObject);
	}

    void FixedUpdate () 
    {
        float x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        float y = Input.GetAxis(Constants.VERTICAL_AXIS);
        rigidbody.velocity = new Vector2(x, y);

        if (isGrounded() && Input.GetButtonDown(Constants.JUMP)) {
            rigidbody.velocity += new Vector2(0, jumpSpeed);
        }
    }

    private bool isGrounded() 
    {
        return Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.1f, layerMask);
    }

    private void DeathByFallingObject(Hashtable h) 
    {
        Debug.Log("show death animation");
        EventManager.TriggerEvent(Constants.PLAYER_DIED_EVENT);
    }

    public void SpawnPlayer(Vector2 position)
    {
        transform.position = position;
    }
}
