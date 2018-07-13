using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCheck : MonoBehaviour {
	SpriteRenderer sprite; 

	public Sprite AliveTree;
	public Sprite DeadTree;

	private void OnTriggerEnter2D(Collider2D other)
    {
		object [] gObj = GameObject.FindGameObjectsWithTag("Tree");
		foreach (object o in gObj){
			GameObject g = (GameObject) o;
			sprite = g.GetComponent<SpriteRenderer>();
			if (GameObject.Find("Held Flowers") != null) {
				sprite.sprite = AliveTree;
			}
			if (GameObject.Find("Held Flowers") == null){
				sprite.sprite = DeadTree;
			}
		}
    }
}
