using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectParent : MonoBehaviour {
    FallingObject fallingObject;

	// Use this for initialization
	void Start () {
        fallingObject = transform.Find("Falling Object").GetComponent<FallingObject>();
	}
	
    public void TriggerFall() 
    {
        fallingObject.TriggerFall();
    }
}
