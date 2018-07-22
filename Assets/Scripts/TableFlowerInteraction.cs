using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFlowerInteraction : MonoBehaviour
{
    GameObject tableFlowers;

    private void Start()
    {
        tableFlowers = GameObject.Find("TableFlowers");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG)
        {
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller.HasFlowers()) {
                controller.RemoveFlowers();
                tableFlowers.GetComponent<SpriteRenderer>().enabled = true;   
            }
        }
    }
}
