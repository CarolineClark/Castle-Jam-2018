using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    public bool yMatchX = true;
    public float xParallax = 0.5f;
    public float yParallax = 1.0f;
    public bool wrapOn = false;

    private Vector3 offset;
    private Vector2 previousPos;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        offset = transform.position;
        if (yMatchX) {
            yParallax = xParallax;
        }
    }

    void AdjustBackground() {
        Vector3 cameraPos = Camera.main.transform.position;
        var img = transform.position;
        var delta = new Vector2(cameraPos.x - img.x, cameraPos.y - img.y);
        float halfWidth = spriteRenderer.bounds.size.x * .5f;
        float quarterWidth = halfWidth * .5f;
        float moveWidth = halfWidth;
        if (xParallax != 0) {
            moveWidth = halfWidth / xParallax;
        }
        if (delta.x > quarterWidth) {
            offset = new Vector3(offset.x + moveWidth, offset.y, offset.z);
        } else if (delta.x < -quarterWidth) {
            offset = new Vector3(offset.x - moveWidth, offset.y, offset.z);
        }
    }

    void LateUpdate() 
    {
        Vector2 cameraPos = Camera.main.transform.position;
        Vector2 delta;

        if (wrapOn) {
            AdjustBackground();
        }

        delta = new Vector2(cameraPos.x - offset.x, cameraPos.y - offset.y);
        transform.position = new Vector3(delta.x * xParallax + offset.x, delta.y * yParallax + offset.y, transform.position.z);
    }
}
