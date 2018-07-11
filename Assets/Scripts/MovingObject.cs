using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public Transform startMarker;
    public Transform endMarker;
	public float speed = 1.0f;

	private float startTime; 
	private float journeyLength;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector2.Lerp(startMarker.position, endMarker.position, fracJourney);
	}
}
