using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    private BoxCollider2D collider2D;

	void Start () {
        collider2D = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision detected");
        if (collision.tag == Constants.PLAYER_TAG) 
        {
            Debug.Log("player hit");
            EventManager.TriggerEvent(Constants.FALLING_OBJECT_HIT_EVENT);
        }
    }
}
