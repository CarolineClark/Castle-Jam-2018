﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	public float jumpBuffer = 1.3f;
    public int maxAngle = 60;

    private int groundLayerMask = 1 << Constants.GROUND_LAYER;
    private int signLayerMask = 1 << Constants.SIGN_LAYER;
    private int platformLayerMask = 1 << Constants.PLATFORM_LAYER;
    private int playerLayerMask = 1 << Constants.PLAYER_LAYER;
    private int groundSignLayerMask;
    private int groundSignPlatformMask;
    private int groundSignLayerPlayerMask;

    private void Start()
    {
        groundSignLayerMask = groundLayerMask | signLayerMask;
        groundSignPlatformMask = groundLayerMask | signLayerMask | platformLayerMask;
        groundSignLayerPlayerMask = groundSignLayerMask | playerLayerMask;
    }

    public bool RaycastHitsGround() {
        return RaycastHitsLayerMask(groundLayerMask);
    }

    public bool RaycastHitsGroundSign()
    {
        return RaycastHitsLayerMask(groundSignLayerMask);
    }

    public bool RaycastHitsGroundSignPlayer()
    {
        return RaycastHitsLayerMask(groundSignLayerPlayerMask);
    }

    public bool RaycastHitsGroundSignPlatform()
    {
        return RaycastHitsLayerMask(groundSignPlatformMask);
    }

    private bool RaycastHitsLayerMask(int layerMask)
    {
        //Debug.Log(finalmask);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), jumpBuffer, layerMask);
        if (hit.collider != null)
        {
            float angle = Vector2.Angle(hit.normal, new Vector2(0, 1));
            return (Mathf.Abs(angle) < maxAngle);
        }
        return false;
    }
}
