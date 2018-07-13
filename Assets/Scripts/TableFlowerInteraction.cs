using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFlowerInteraction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (GameObject.Find("Held Flowers") != null){
			GameObject player = GameObject.Find("Held Flowers");
			player.GetComponent<SpriteRenderer>().enabled = false;
			GameObject tableFlowers = GameObject.Find("TableFlowers");
			tableFlowers.GetComponent<SpriteRenderer>().enabled = true;
		}
    }
}
