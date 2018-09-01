using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    public string text = "Some default text";

    private Rigidbody2D rb;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer speechbubbleSprite;
    private SpeechbubbleController controller;

    private static string SPEECHBUBBLE = "Speechbubble";
    private float runningSpeed1 = 7f;
    private float runningSpeed2 = 10f;
    private float maxMagnitude1 = 3f;
    private float maxMagnitude2 = 5f;
    private float jumpSpeed = 20f;
    private float maxY = 3f;

	void Start () {
        controller = transform.Find(SPEECHBUBBLE).GetComponent<SpeechbubbleController>();
        player = GameObject.Find(Constants.PLAYER_TAG);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ShowText());
	}

    private IEnumerator ShowText() {
        while (true) {
            controller.ShowWithText(text);

            yield return new WaitForSeconds(5);

            controller.Hide();

            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(Constants.PLAYER_TAG)) {
            // send event? If player is here for 5 seconds, then kill them? Slow them down?
            Debug.Log("player is close to fear");
        }
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
