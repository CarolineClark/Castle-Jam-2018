﻿using System.Collections;
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
        offset = transform.position;// - Camera.main.transform.position;
        if (yMatchX) {
            yParallax = xParallax;
        }
    }

    private void FixedUpdate()
    {
        if (wrapOn) {
            AdjustBackground();   
        }
    }

    void AdjustBackground() {
        Vector3 cameraPosition = Camera.main.transform.position;
        //Debug.Log("cameraPosition.x = " + cameraPosition.x + ", transform.position.x = " + transform.position.x + ", spriteRenderer.size.x = " + spriteRenderer.size.x);
        float x = spriteRenderer.bounds.size.x/2;
        if (cameraPosition.x < transform.position.x)
        {
            if (cameraPosition.x < transform.position.x - x/2)
            {
                //Debug.Log("offset changed");
                offset = new Vector3(offset.x - x, offset.y, offset.z);
            }
        }
        else if (cameraPosition.x > transform.position.x)
        {
            if (cameraPosition.x > transform.position.x + x/2)
            {
                //Debug.Log("offset changed");
                offset = new Vector3(offset.x + x, offset.y, offset.z);
            }
        }
    }

    void LateUpdate() 
    {
        Vector2 pos = Camera.main.transform.position;
        transform.position = new Vector3((pos.x - offset.x) * xParallax + offset.x, (pos.y - offset.y) * yParallax + offset.y, transform.position.z);
    }
}
