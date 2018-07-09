using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int direction = RIGHT;

    private float jumpSpeed = 10f;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private int layerMask;
    private Animator animator;

    private const int RIGHT = 1;
    private const int LEFT = -1;

    void Start () 
    {
        layerMask = 1 << Constants.GROUND_LAYER;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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

        UpdateImage(x, y);
    }

    private void UpdateImage (float inputX, float inputY) {
        float xspeed = rigidbody.velocity.x;
        if (xspeed > 0) { direction = RIGHT; }
        if (xspeed < 0) { direction = LEFT; }
        this.transform.localScale = new Vector3(direction, 1, 1);

        // TODO Bug with idle animation appearing at incorrect time.
        // When we turn around the player briefly flashes to an idle animation
        // seeming to suggest that inputX is not directly related to input
        // but instead interpolated over time.
        bool running = xspeed != 0 || inputX != 0;
        animator.SetBool("Running", running);
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
