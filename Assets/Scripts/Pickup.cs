using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public enum PickupType { Flowers };
    public PickupType pickupType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        playerController.UpdateInventory(pickupType);
    }
}
