using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCheck : MonoBehaviour {
	SpriteRenderer sprite; 

	public Sprite AliveTree;
	public Sprite DeadTree;
	public Sprite AliveBush;
	public Sprite DeadBush;

    private object[] gTObj;
    private object[] gBObj;

    private void Start()
    {
        gTObj = GameObject.FindGameObjectsWithTag("Tree");
        gBObj = GameObject.FindGameObjectsWithTag("Bush");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG) {
            PlayerController controller = other.GetComponent<PlayerController>();

            foreach (object o in gTObj)
            {
                GameObject g = (GameObject)o;
                sprite = g.GetComponent<SpriteRenderer>();
                if (controller.HasFlowers())
                {
                    sprite.sprite = AliveTree;
                }
                else
                {
                    sprite.sprite = DeadTree;
                }
            }

            foreach (object o in gBObj)
            {
                GameObject g = (GameObject)o;
                sprite = g.GetComponent<SpriteRenderer>();
                if (controller.HasFlowers())
                {
                    sprite.sprite = AliveBush;
                }
                else
                {
                    sprite.sprite = DeadBush;
                }
            }  
        }
    }
}
