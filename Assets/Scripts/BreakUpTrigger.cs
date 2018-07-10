using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUpTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            Debug.Log("Executing break up");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.freezeInput = true;
            playerController.isSurprised = true;
            Destroy(gameObject);
        }
    }
}
