using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG) {
            // send message to player that player should lose control
            EventManager.TriggerEvent(Constants.END_GAME);

            // show UI element for credits
            Hashtable h2 = new Hashtable();
            h2.Add(Constants.CAMERA_CHANGE_VIEWPORT, 5.0f);
            EventManager.TriggerEvent(Constants.CAMERA_CHANGE_VIEWPORT, h2);

        }
    }
}
