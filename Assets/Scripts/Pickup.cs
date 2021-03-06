﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public enum PickupType { Flowers };
    public PickupType pickupType;
    public bool pickupToggle = true;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG && pickupToggle) {
            SoundManager.instance.PlayFx(pickupSound);
            gameObject.SetActive(false);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.UpdateInventory(pickupType);
        }
    }
}
