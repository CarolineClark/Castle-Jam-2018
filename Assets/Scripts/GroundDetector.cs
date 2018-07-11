using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	private float jumpBuffer = 1.3f;
	private int layerMask = 1 << Constants.GROUND_LAYER;
    private int maxAngle = 60;

	public bool RaycastHitsGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), jumpBuffer, layerMask);
        if (hit.collider != null)
        {
            float angle = Vector2.Angle(hit.normal, new Vector2(0, 1));
            return (Mathf.Abs(angle) < maxAngle);
        }
        return false;
    }
}
