using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectTrigger : MonoBehaviour {
    private FallingObjectParent parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<FallingObjectParent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            parent.TriggerFall();
        }
    }
}
