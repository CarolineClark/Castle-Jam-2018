using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private float jumpSpeed = 20f;
    private float runningSpeed = 7f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int layerMask;
    private Animator animator;
    private string RUNNING_ANIM = "Running";
    private string GROUNDED_ANIM = "Grounded";
    private string DEAD_ANIM = "Dead";
    private Vector3 offset = new Vector3(0, 0, -40);
    private bool dead = false;

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
        if (dead) {
            return;
        }

        float x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        rb.velocity = new Vector2(x * runningSpeed, rb.velocity.y);

        bool grounded = isGrounded();
        if (grounded && Input.GetButtonDown(Constants.JUMP)) {
            rb.velocity = rb.velocity + new Vector2(0.0f, jumpSpeed);
        }

        UpdateImage(x, grounded);
        Camera.main.transform.position = transform.position + offset;
    }

    private void UpdateImage (float inputX, bool grounded) {
        float xspeed = rb.velocity.x;
        spriteRenderer.flipX = xspeed < 0;
        bool running = !CloseToZero(xspeed) || !CloseToZero(inputX);
        animator.SetBool(RUNNING_ANIM, running);
        animator.SetBool(GROUNDED_ANIM, grounded);
    }

    private bool CloseToZero(float num) {
        return System.Math.Abs(num) < 0.01f;
    }

    private bool isGrounded() 
    {
        return CloseToZero(rb.velocity.y) || RaycastHitsGround();
    }

    private bool RaycastHitsGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 2.0f, layerMask);
        if (hit.collider != null)
        {
            float angle = Vector2.Angle(hit.normal, new Vector2(0, 1));
            return (Mathf.Abs(angle) < 45);
        }
        return false;
    }

    private void DeathByFallingObject(Hashtable h) 
    {
        Kill();
    }

    public void Kill() {
        dead = true;
        animator.SetBool(DEAD_ANIM, true);
        EventManager.TriggerEvent(Constants.PLAYER_DIED_EVENT);
    }

    public void SpawnPlayer(Vector2 position)
    {
        dead = false;
        transform.position = position;
        // Reset animations
        if (animator.isInitialized) {
            animator.Rebind();
        }
    }
}
