using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    Rigidbody2D rb;
    GameObject player;
    SpriteRenderer spriteRenderer;
    private float runningSpeed1 = 7f;
    private float runningSpeed2 = 10f;
    private float maxMagnitude1 = 3f;
    private float maxMagnitude2 = 5f;
    private float jumpSpeed = 20f;
    private float maxY = 3f;

	void Start () {
        player = GameObject.Find(Constants.PLAYER_TAG);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    void Update()
    {
        Vector2 pos = transform.position - player.transform.position;
        if (pos.magnitude > maxMagnitude2)
        {
            Running(runningSpeed2);
        }
        else if (pos.magnitude > maxMagnitude1)
        {
            Running(runningSpeed1);
        }

        //animator.SetBool(RUNNING_ANIM_FLAG, (rb.velocity.magnitude > EPISLON));
    }

    private void Running(float speed)
    {
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

    private void StepRight(float speed)
    {
        spriteRenderer.flipX = false;
        rb.velocity = new Vector2(speed, rb.velocity.y);
        //animator.SetBool(RUNNING_ANIM_FLAG, (rb.velocity.magnitude > EPISLON));
    }

    private void StepLeft(float speed)
    {
        spriteRenderer.flipX = true;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        //animator.SetBool(RUNNING_ANIM_FLAG, (rb.velocity.magnitude > EPISLON));
    }

    private void SingleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
}
