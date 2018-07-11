using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public enum PickupType { Flowers };
    public PickupType pickupType;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.instance.PlaySingle(pickupSound);
        gameObject.SetActive(false);
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        playerController.UpdateInventory(pickupType);
    }
}
