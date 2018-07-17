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
    private List<GroundDetector> groundDetectors = new List<GroundDetector>();
    private string groundDetectorLeft = "Sensor-left";
    private string groundDetectorRight = "Sensor-right";
    private bool shookCamera = false;
    private bool hasHitGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dropShadow = Instantiate(dropShadowPrefab);
        groundDetectors.Add(findGroundDetectorByName(groundDetectorLeft));
        groundDetectors.Add(findGroundDetectorByName(groundDetectorRight));
    }

    private GroundDetector findGroundDetectorByName(string childName)
    {
        return transform.Find(childName).gameObject.GetComponent<GroundDetector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG && !hasHitGround)
        {
            Debug.Log("hit player");
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
        bool grounded = false;
        foreach (GroundDetector detector in this.groundDetectors)
        {
            grounded = grounded || detector.RaycastHitsGroundSignPlayer();
        }
        return grounded;
    }

    private void PlaceDropshadow() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 100.0F, 1<< Constants.GROUND_LAYER);
        if (hit.collider != null) {
            dropShadow.SetActive(true);
            dropShadow.transform.position = transform.position + (hit.distance * new Vector3(0, -1, 0));   
        } else {
            dropShadow.SetActive(false);
        }
    }
}
