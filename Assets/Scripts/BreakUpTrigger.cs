﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUpTrigger : MonoBehaviour {
    public Vector2 signLocation;
    public GameObject fissureLeft;
    public GameObject fissureRight;
    public GameObject platformLeftObject;
    public GameObject platformRightObject;
    public float shakeCameraAmount = .6f;
    public AudioClip splitWorldSound;

    private PlayerController player;
    private PartnerFollowPlayer partner;

    private void Start()
    {
        partner = GameObject.FindWithTag(Constants.PARTNER_TAG).GetComponent<PartnerFollowPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            Debug.Log("Executing break up");
            player = collision.gameObject.GetComponent<PlayerController>();
            StartCoroutine(DoBreakUp());
        }
    }

    private IEnumerator DoBreakUp() {
        // Freeze the player and change them to a surprised state
        player.freezeInput = true;
        player.isSurprised = true;
        CameraController.StopFollowing(player.gameObject);

        yield return new WaitForSeconds(2);

        // Reveal the 'I don't love you' sign
        player.isSurprised = false;
        player.isSad = true;
        partner.RunRight();
        Vector3 worldSignLocation = transform.parent.TransformPoint(signLocation);
        CameraController.Target(worldSignLocation);
        SoundManager.instance.PlayFx(splitWorldSound);

        yield return new WaitForSeconds(3);

        // Make the fissure visible
        CameraController.Shake(shakeCameraAmount);
        player.isSad = false;
        player.isSurprised = true;
        fissureLeft.SetActive(true);
        fissureRight.SetActive(true);

        yield return new WaitForSeconds(3);

        // Return control back to the player, follow them
        // with the camera again
        float animationTime = 5f;
        var moveAmount = new Vector2(-40f, -4f);
        float rotateAmount = 0.1745f;
        iTween.RotateBy(platformLeftObject, new Vector3(0f,0f,rotateAmount), animationTime);
        iTween.MoveBy(platformLeftObject, new Vector3(moveAmount.x, moveAmount.y, 0f), animationTime);
        iTween.RotateBy(platformRightObject, new Vector3(0f,0f,-rotateAmount), animationTime);
        iTween.MoveBy(platformRightObject, new Vector3(-moveAmount.x, moveAmount.y, 0f), animationTime);
        
        CameraController.StopShaking();
        CameraController.Follow(player.gameObject);
        player.freezeInput = false;
        player.isSurprised = false;
        Destroy(gameObject);
    }
}
