using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectInstantiate : MonoBehaviour
{
    public float screenShake;
    public AudioClip landingSound1;
    public AudioClip landingSound2;
    private Rigidbody2D rb;
    public GameObject dropShadowPrefab;

    private GameObject dropShadow;
    private GroundDetector groundDetector;
    private bool shookCamera = false;
    private bool hasHitGround = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dropShadow = Instantiate(dropShadowPrefab);
        groundDetector = transform.Find("Ground Detector").gameObject.GetComponent<GroundDetector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG && !hasHitGround)
        {
            //Debug.Log("hit player");
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }

    private void FixedUpdate()
    {
        bool grounded = IsGrounded();
        if (!grounded && dropShadow != null) {
            PlaceDropshadow();
        }

        if (grounded) {
            hasHitGround = true;
            Destroy(dropShadow);
            dropShadow = null;
        }
        if (grounded && !shookCamera) {
            shookCamera = true;
            CameraController.Shake(screenShake);
            SoundManager.instance.PlaySignCrashRandom(landingSound1, landingSound2);
            gameObject.layer = Constants.GROUND_LAYER;
        }
    }

    private bool Moving()
    {
        return System.Math.Abs(rb.velocity.y) > 0.01f;
    }

    private bool IsGrounded()
    {
        return groundDetector.IsTouchingGroundSignPlayer();
    }

    private void PlaceDropshadow() {
        //float y = transform.localScale.y / spriteRenderer.sprite.bounds.size.y;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - 2f);
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(0, -1), 100.0F, Constants.GROUND_SIGN_LAYER_MASK);
        if (hit.collider != null) {
            dropShadow.SetActive(true);
            dropShadow.transform.position = transform.position + (hit.distance * new Vector3(0, -1, 0) - new Vector3(0, 2f, 0));   
        } else {
            dropShadow.SetActive(false);
        }
    }
}
