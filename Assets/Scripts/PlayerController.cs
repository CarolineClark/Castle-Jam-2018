using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public bool freezeInput = false;
    public bool isSurprised = false;
    public AudioClip footstepSound1;
    public AudioClip footstepSound2;
    public AudioClip footstepSound3;

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
    private const string SURPRISE_OBJECT_NAME = "Surprise";
    private GameObject surprise;
    private bool hasDoubleJump = true;
    private float defaultDistance = 0.01f;
    private const string HELD_FLOWERS_OBJECT_NAME = "Held Flowers";
    private GameObject heldFlowers;
    private Dictionary<Pickup.PickupType, int> inventory = new Dictionary<Pickup.PickupType, int>();

    private void ResetInventory()
    {
        inventory = new Dictionary<Pickup.PickupType, int>
        {
            { Pickup.PickupType.Flowers, 0 }
        };
    }

    void Start () 
    {
        layerMask = 1 << Constants.GROUND_LAYER;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EventManager.StartListening(Constants.FALLING_OBJECT_HIT_EVENT, DeathByFallingObject);
        surprise = gameObject.transform.Find(SURPRISE_OBJECT_NAME).gameObject;
        heldFlowers = gameObject.transform.Find(HELD_FLOWERS_OBJECT_NAME).gameObject;
        ResetInventory();
        CameraController.Follow(gameObject);
    }

    void FixedUpdate () 
    {
        float x = 0;
        bool jumping = false;
        bool grounded = isGrounded();

        if (grounded) {
            hasDoubleJump = true;
        }
        if (!freezeInput) {
            x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
            jumping = Input.GetButtonDown(Constants.JUMP);
        }

        bool movingHorizontally = x != 0;
        bool noSoundPlaying = !SoundManager.instance.footstepSource.isPlaying;
        if (grounded && movingHorizontally && noSoundPlaying)
        {
            SoundManager.instance.PlayFootstep(footstepSound1, footstepSound2, footstepSound3);
        }

        rb.velocity = new Vector2(x * runningSpeed, rb.velocity.y);
        if (grounded && jumping || hasDoubleJump && jumping) {
            if (!grounded) {
                hasDoubleJump = false;
            } 
            float currentY = System.Math.Abs(rb.velocity.y) * 1;
            rb.velocity = rb.velocity + new Vector2(0.0f, jumpSpeed + currentY);
        }
        UpdateImage(x, grounded);
    }

    private void UpdateImage (float inputX, bool grounded) {
        float xspeed = rb.velocity.x;
        float yspeed = rb.velocity.y;
        spriteRenderer.flipX = xspeed < 0;

        // Ensure that we only flip the sprite
        // when the player is moving (xspeed != 0)
        if (xspeed != 0) {
            spriteRenderer.flipX = xspeed < 0;
        }

        bool running = !CloseToZero(xspeed, defaultDistance) || !CloseToZero(inputX, defaultDistance);
        animator.SetBool(RUNNING_ANIM, running);
        animator.SetBool(GROUNDED_ANIM, grounded);

        if (yspeed < -50) {
            Kill();
        }
        surprise.SetActive(isSurprised);
        heldFlowers.SetActive(inventory[Pickup.PickupType.Flowers] > 0);
    }

    private bool CloseToZero(float num, float epsilon) {
        return System.Math.Abs(num) < epsilon;
    }

    private bool isGrounded() 
    {
        return CloseToZero(rb.velocity.y, defaultDistance) || RaycastHitsGround(45);
    }
    private bool RaycastHitsGround(float maxAngle) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 3.0f, layerMask);
        if (hit.collider != null)
        {
            float angle = Vector2.Angle(hit.normal, new Vector2(0, 1));
            return (Mathf.Abs(angle) < maxAngle);
        }
        return false;
    }

    private void DeathByFallingObject(Hashtable h) 
    {
        Kill();
    }

    public void Kill() {
        freezeInput = true;
        animator.SetBool(DEAD_ANIM, true);
        EventManager.TriggerEvent(Constants.PLAYER_DIED_EVENT);
    }

    public void SpawnPlayer(Vector2 position)
    {
        freezeInput = false;
        transform.position = position;
        rb.velocity = new Vector2 (0,0);
        // Reset animations
        if (animator.isInitialized) {
            animator.SetBool(DEAD_ANIM, false);
            animator.SetBool(GROUNDED_ANIM, true);
        }
    }

    public void UpdateInventory(Pickup.PickupType pickup)
    {
        inventory[pickup]++;
        Debug.Log("Picked up " + pickup + " - You now have " + inventory[pickup]);
    }
}
