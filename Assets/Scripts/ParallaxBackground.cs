using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    public float xParallax = 0.5f;
    public float yParallax = 1.0f;

    private Vector3 offset;
    private Vector2 previousPos;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        offset = transform.position - Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        AdjustBackground();
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
        transform.position = new Vector3(pos.x * xParallax + offset.x, pos.y * yParallax + offset.y, transform.position.z);
    }

    private bool IsVisibleFrom(Renderer rendererParam, Camera cameraParam)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cameraParam);
        return GeometryUtility.TestPlanesAABB(planes, rendererParam.bounds);
    }
}
