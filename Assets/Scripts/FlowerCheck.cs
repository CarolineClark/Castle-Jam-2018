using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCheck : MonoBehaviour {
	SpriteRenderer sprite; 

	public Sprite AliveTree;
	public Sprite DeadTree;
	public Sprite AliveBush;
	public Sprite DeadBush;

	private void OnTriggerEnter2D(Collider2D other)
    {
		object [] gTObj = GameObject.FindGameObjectsWithTag("Tree");
		foreach (object o in gTObj){
			GameObject g = (GameObject) o;
			sprite = g.GetComponent<SpriteRenderer>();
			if (GameObject.Find("Held Flowers") != null) {
				sprite.sprite = AliveTree;
			}
			if (GameObject.Find("Held Flowers") == null){
				sprite.sprite = DeadTree;
			}
		}

		object [] gBObj = GameObject.FindGameObjectsWithTag("Bush");
		foreach (object o in gBObj){
			GameObject g = (GameObject) o;
			sprite = g.GetComponent<SpriteRenderer>();
			if (GameObject.Find("Held Flowers") != null) {
				sprite.sprite = AliveBush;
			}
			if (GameObject.Find("Held Flowers") == null){
				sprite.sprite = DeadBush;
			}
		}
    }
}
