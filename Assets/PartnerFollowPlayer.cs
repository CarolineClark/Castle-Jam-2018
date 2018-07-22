using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowPlayer : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private GameObject player;
    private float maxMagnitude1 = 3f;
    private float maxMagnitude2 = 5f;
    private float maxY = 3f;
    private float jumpSpeed = 20f;
    private float runningSpeed1 = 7f;
    private float runningSpeed2 = 10f;
    private string RUNNING_ANIM_FLAG = "Running";
    private float EPISLON = 0.1f;

	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find(Constants.PLAYER_TAG);
        playerRb = player.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        Vector2 pos = transform.position - player.transform.position;
        if (pos.magnitude > maxMagnitude2) {
            Running(runningSpeed2);
        } else if (pos.magnitude > maxMagnitude1) {
            Running(runningSpeed1);
        }

        animator.SetBool(RUNNING_ANIM_FLAG, (rb.velocity.magnitude > EPISLON));
	}

    private void Running(float speed) {
        if (transform.position.x > player.transform.position.x)
        {
            StepLeft(speed);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            StepRight(speed);
        }
        if (transform.position.y < player.transform.position.y - maxY)
        {
            SingleJump();
        }
    }

    private void SingleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    void StepLeft(float speed) {
        spriteRenderer.flipX = true;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void StepRight(float speed)
    {
        spriteRenderer.flipX = false;
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
