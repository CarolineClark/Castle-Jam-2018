using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate () {
        float x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        float y = Input.GetAxis(Constants.VERTICAL_AXIS);
        rigidbody.velocity = new Vector2(x, y);
    }
}
