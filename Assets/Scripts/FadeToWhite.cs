using UnityEngine;

public class FadeToWhite : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) {
            EventManager.TriggerEvent(Constants.FADE_TO_WHITE);
        }
    }
}
