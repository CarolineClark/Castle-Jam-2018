using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour {
    private static string CREATURE_NAME = "Creature";
    private GameObject monster;
    private Collider2D collider2D;

	void Start () {
        // child of the gameobject will be disabled.
        monster = transform.GetChild(0).gameObject;
        monster.SetActive(false);
        collider2D = GetComponent<Collider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(Constants.PLAYER_TAG)) {
            monster.SetActive(true);
            collider2D.enabled = false;
        }
    }
}
