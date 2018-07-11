using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
    public int musicNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG) {
            SoundManager.instance.SetMusic(musicNumber);
        }
    }
}
