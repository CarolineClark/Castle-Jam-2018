using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int direction = RIGHT;

    private float jumpSpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int layerMask;
    private Animator animator;

    private const int RIGHT = 1;
    private const int LEFT = -1;
    private string RUNNING_ANIM = "Running";

    void Start () 
    {
        layerMask = 1 << Constants.GROUND_LAYER;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EventManager.StartListening(Constants.FALLING_OBJECT_HIT_EVENT, DeathByFallingObject);
	}

    void FixedUpdate () 
    {
        float x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        float y = Input.GetAxis(Constants.VERTICAL_AXIS);
        rb.velocity = new Vector2(x, y);

        if (isGrounded() && Input.GetButtonDown(Constants.JUMP)) {
            rb.velocity += new Vector2(0, jumpSpeed);
        }

        UpdateImage(x, y);
    }

    private void UpdateImage (float inputX, float inputY) {
        float xspeed = rb.velocity.x;
        spriteRenderer.flipX = xspeed < 0;
        bool running = CloseToZero(xspeed) || CloseToZero(inputX);
        animator.SetBool(RUNNING_ANIM, running);
    }

    private bool CloseToZero(float num) {
        return System.Math.Abs(num) > 0.01f;
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
