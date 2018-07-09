using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectParent : MonoBehaviour {
    privtae static string CHILD_OBJECT_NAME = "Falling Object";
    FallingObject fallingObject;

	// Use this for initialization
	void Start () {
        fallingObject = transform.Find(CHILD_OBJECT_NAME).GetComponent<FallingObject>();
	}
	
    public void TriggerFall() 
    {
        fallingObject.TriggerFall();
    }
}
