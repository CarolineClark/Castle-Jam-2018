using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public bool startFallenDown = false;
    public bool freezeInput = false;
    public bool isSurprised = false;
    public AudioClip footstepSound1;
    public AudioClip footstepSound2;
    public AudioClip footstepSound3;
    public AudioClip jumpSound;
    public AudioClip jumpSound2;
    public AudioClip landJumpSound;
    public AudioClip deathByFallingSound;
    public AudioClip deathByFallingObjectSound;
    public float runningSpeed = 7f;

    private float jumpSpeed = 20f;
    private float jumpBuffer = 2.0f;
    private float secondJumpSpeed = 15f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private string RUNNING_ANIM = "Running";
    private string GROUNDED_ANIM = "Grounded";
    private string DEAD_ANIM = "Dead";
    private string SURPRISED_ANIM = "Surprised";
    private string IDLE_ANIM_STATE = "PlayerIdle";
    private string GET_UP_ANIM_STATE = "PlayerGetUp";
    private Vector3 offset = new Vector3(0, 0, -40);
    private const string SURPRISE_OBJECT_NAME = "Surprise";
    private GameObject surprise;
    private const string HELD_FLOWERS_OBJECT_NAME = "Held Flowers";
    private GameObject heldFlowers;
    private Dictionary<Pickup.PickupType, int> inventory = new Dictionary<Pickup.PickupType, int>();
    private int numOfFramesGracePeriod = 6;
    private List<GroundDetector> groundDetectors = new List<GroundDetector>();
    private string groundDetectorLeft = "Sensor-left";
    private string groundDetectorMiddle = "Sensor-middle";
    private string groundDetectorRight = "Sensor-right";
    private int numberOfSignsToBury = 7;

    private int counter = 0;
    private bool groundedWithGracePeriod = false;
    private bool groundedInPrevFrame = true;

    private static float EPSILON = 0.01f;
    private float timeCounter;

    private void ResetInventory()
    {
        inventory = new Dictionary<Pickup.PickupType, int>
        {
            { Pickup.PickupType.Flowers, 0 }
        };
    }

    void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EventManager.StartListening(Constants.FALLING_OBJECT_HIT_EVENT, DeathByFallingObject);
        EventManager.StartListening(Constants.SET_PLAYER_SPEED, SetPlayerSpeed);
        surprise = gameObject.transform.Find(SURPRISE_OBJECT_NAME).gameObject;
        heldFlowers = gameObject.transform.Find(HELD_FLOWERS_OBJECT_NAME).gameObject;
        ResetInventory();
        CameraController.Follow(gameObject);

        groundDetectors.Add(findGroundDetectorByName(groundDetectorLeft));
        groundDetectors.Add(findGroundDetectorByName(groundDetectorMiddle));
        groundDetectors.Add(findGroundDetectorByName(groundDetectorRight));

        if (startFallenDown) {
            animator.Play(GET_UP_ANIM_STATE);
            freezeInput = true;
        }
    }

    private void SetPlayerSpeed(Hashtable h) {
        if (h.ContainsKey(Constants.SET_PLAYER_SPEED)) {
            timeCounter = 0f;
            StartCoroutine(AdjustPlayerSpeed(runningSpeed, (float)h[Constants.SET_PLAYER_SPEED], 15f));
        }
    }

    private IEnumerator AdjustPlayerSpeed(float startPlayerSpeed, float endPlayerSpeed, float floatTimeTransition) {
        while (timeCounter < floatTimeTransition) {
            timeCounter += Time.deltaTime;
            runningSpeed = Mathf.Lerp(startPlayerSpeed, endPlayerSpeed, timeCounter / floatTimeTransition);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
    }

    // This function is called by an animation event in PlayerGetUp.anim
    // It is used to reactivate input after the player gets up off the floor.
    public void OnPlayerGetUpComplete(AnimationEvent ev) {
        freezeInput = false;
    }

    private GroundDetector findGroundDetectorByName(string childName) {
        return transform.Find(childName).gameObject.GetComponent<GroundDetector>();
    }

    void FixedUpdate () 
    {
        float x = 0;
        bool jumping = false;
        bool grounded = isGrounded();

        bool landedThisFrame = !groundedInPrevFrame && grounded;
        if (landedThisFrame)
        {
            SoundManager.instance.PlayFx(landJumpSound);
        }
        groundedInPrevFrame = grounded;

        DecideIfGroundedWithGracePeriod(grounded);

        if (!freezeInput) {
            x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
            jumping = Input.GetButtonDown(Constants.JUMP);
        }

        bool movingHorizontally = !CloseToZero(x, EPSILON);
        bool noSoundPlaying = !SoundManager.instance.footstepSource.isPlaying;
        if (grounded && movingHorizontally && noSoundPlaying)
        {
            SoundManager.instance.PlayFootstep(footstepSound1, footstepSound2, footstepSound3);
        }
        rb.velocity = new Vector2(x * runningSpeed, rb.velocity.y);
        SingleJump(groundedWithGracePeriod, jumping);
        UpdateImage(x, grounded);
        counter += 1;

        if (IsBuriedBySigns()) {
            Kill();
        }
    }

    private bool DecideIfGroundedWithGracePeriod(bool grounded) {
        // was grounded in the last grace period number of frames
        if (grounded)
        {
            groundedWithGracePeriod = grounded;
            counter = 0;
        }
        else if (counter > numOfFramesGracePeriod)
        {
            groundedWithGracePeriod = false;
        }
        return groundedWithGracePeriod;
    }

    private void SingleJump(bool grounded, bool jumping)
    {
        if (grounded && jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            SoundManager.instance.PlayFxRandom(jumpSound, jumpSound2);
        }
    }

    private void UpdateImage (float inputX, bool grounded) {
        float xspeed = rb.velocity.x;
        float yspeed = rb.velocity.y;
        spriteRenderer.flipX = xspeed < 0;

        // Ensure that we only flip the sprite
        // when the player is moving (xspeed is not close to 0)
        if (!CloseToZero(xspeed, EPSILON)) {
            spriteRenderer.flipX = xspeed < 0;
        }

        bool running = !CloseToZero(xspeed, EPSILON) || !CloseToZero(inputX, EPSILON);
        animator.SetBool(RUNNING_ANIM, running);
        animator.SetBool(GROUNDED_ANIM, grounded);

        if (yspeed < -50) {
            bool alreadyDying = animator.GetBool(DEAD_ANIM);
            if (!alreadyDying)
            {
                SoundManager.instance.PlayFx(deathByFallingSound);
                Kill();
            }
        }
        surprise.SetActive(isSurprised);
        animator.SetBool(SURPRISED_ANIM, isSurprised);
        heldFlowers.SetActive(inventory[Pickup.PickupType.Flowers] > 0);
    }

    private bool CloseToZero(float num, float epsilon) {
        return System.Math.Abs(num) < epsilon;
    }

    private bool isGrounded() 
    {
        bool grounded = false;
        foreach (GroundDetector detector in this.groundDetectors) {
            grounded = grounded || detector.RaycastHitsGroundSignPlatform();
        }
        return grounded;
    }

    private void DeathByFallingObject(Hashtable h) 
    {
        SoundManager.instance.PlayFx(deathByFallingObjectSound);
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

    private bool IsBuriedBySigns() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(0, 1), 100.0F, 1 << Constants.SIGN_LAYER);
        return hits.Length > numberOfSignsToBury;
    }
}
