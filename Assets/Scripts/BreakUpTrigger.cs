using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUpTrigger : MonoBehaviour {
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            Debug.Log("Executing break up");
            player = collision.gameObject.GetComponent<PlayerController>();
            player.freezeInput = true;
            player.isSurprised = true;
            StartCoroutine(DoBreakUp());
        }
    }

    private IEnumerator DoBreakUp() {
        yield return new WaitForSeconds(3);

        player.freezeInput = false;
        player.isSurprised = false;
        Destroy(gameObject);
    }
}
