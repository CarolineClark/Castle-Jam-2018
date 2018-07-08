using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Vector2 position;
    private BoxCollider2D boxCollider2D;

    void Start () 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) {
            // add to checkpoint
        }
    }
}
