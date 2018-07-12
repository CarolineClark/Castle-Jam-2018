using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectInstantiate : MonoBehaviour
{
    public float screenShake;
    private Rigidbody2D rb;

    private List<GroundDetector> groundDetectors = new List<GroundDetector>();
    private string groundDetectorLeft = "Sensor-left";
    private string groundDetectorRight = "Sensor-right";
    private bool shookCamera = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetectors.Add(findGroundDetectorByName(groundDetectorLeft));
        groundDetectors.Add(findGroundDetectorByName(groundDetectorRight));
    }

    private GroundDetector findGroundDetectorByName(string childName)
    {
        return transform.Find(childName).gameObject.GetComponent<GroundDetector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Moving() && collision.tag == Constants.PLAYER_TAG)
        {
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded() && !shookCamera) {
            shookCamera = true;
            CameraController.Shake(screenShake);
        }
    }

    private bool Moving()
    {
        return System.Math.Abs(rb.velocity.y) > 0.01f;
    }

    private bool isGrounded()
    {
        bool grounded = false;
        foreach (GroundDetector detector in this.groundDetectors)
        {
            grounded = grounded || detector.RaycastHitsGround();
        }
        return grounded;
    }
}
