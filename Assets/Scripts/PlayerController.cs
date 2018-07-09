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
        bool grounded = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.1f, layerMask);
        Debug.Log(grounded);
        return grounded;
    }
}
