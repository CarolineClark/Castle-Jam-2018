using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public GameObject otherDoor;

    private string ANIMATION_NAME = "opening-door-anim";
    private InputWrapper inputWrapper;
    private GameObject player;
    private Animator animator;
    private Animation animation;
    private bool playingAnimation = false;
    
	void Start () {
        CheckOtherDoorHookedUp();
        inputWrapper = new InputWrapper();
        animator = GetComponent<Animator>();
	}

    private void CheckOtherDoorHookedUp() {
        if (otherDoor == null) {
            Debug.LogError("Door in scene is not hooked up correctly");
        }
    }

    private void Update()
    {
        if (CanTransportPlayer() && inputWrapper.IsDoorKeyPressed() && !playingAnimation) {
            playingAnimation = true;
            StartCoroutine(playAnimationAndTransport());
        }
    }

    private IEnumerator playAnimationAndTransport() {
        player.SetActive(false);
        animator.SetTrigger("open 0");
        yield return new WaitForSeconds(1.5f);
        player.transform.position = otherDoor.transform.position;
        player.SetActive(true);
        player = null;
        playingAnimation = false;
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
