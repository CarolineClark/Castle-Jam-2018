using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectInstantiate : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Moving() && collision.tag == Constants.PLAYER_TAG)
        {
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }

    private bool Moving()
    {
        return System.Math.Abs(rb.velocity.magnitude) > 0.01f;
    }
}
