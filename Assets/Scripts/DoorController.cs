using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public GameObject otherDoor;

    private InputWrapper inputWrapper;
    private GameObject player;
    
	void Start () {
        CheckOtherDoorHookedUp();
        inputWrapper = new InputWrapper();
	}

    private void CheckOtherDoorHookedUp() {
        if (otherDoor == null) {
            Debug.LogError("Door in scene is not hooked up correctly");
        }
    }

    private void Update()
    {
        if (CanTransportPlayer() && inputWrapper.IsDoorKeyPressed()) {
            player.transform.position = otherDoor.transform.position;
        }
    }

    private bool CanTransportPlayer() {
        return player != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(Constants.PLAYER_TAG)) {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals(Constants.PLAYER_TAG))
        {
            player = null;
        }
    }
}
