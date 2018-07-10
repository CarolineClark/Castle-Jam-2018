using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUpTrigger : MonoBehaviour {
    public Vector2 signLocation;

    private PlayerController player;

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
        player.freezeInput = true;
        player.isSurprised = true;
        CameraController.StopFollowing(player.gameObject);

        yield return new WaitForSeconds(2);

        CameraController.Target(signLocation);

        yield return new WaitForSeconds(3);

        CameraController.Follow(player.gameObject);
        player.freezeInput = false;
        player.isSurprised = false;
        Destroy(gameObject);
    }
}
