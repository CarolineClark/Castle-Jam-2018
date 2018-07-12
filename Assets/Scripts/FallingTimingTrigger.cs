using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTimingTrigger : MonoBehaviour {

    public GameObject prefab;
    public float minIntervalTime;
    public float maxIntervalTime;
    public float minHeight = 5;
    public float maxHeight = 10;
    public float minSpeed;
    public float maxSpeed;
    public float screenShake = 0.5f;

    private bool playerInCollider = false;
    private Vector2 playerPosition;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private Coroutine coroutine;

    private void Start()
    {
        EventManager.StartListening(Constants.RESTART_GAME, DestroyInstantiated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) {
            playerInCollider = true;
            coroutine = StartCoroutine(InstantiateFallingObject());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            playerInCollider = false;
            if (coroutine != null) {
                StopCoroutine(coroutine);    
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == Constants.PLAYER_TAG)
        {
            playerPosition = other.transform.position;
        }
    }

    IEnumerator InstantiateFallingObject() {
        while (playerInCollider) {
            float time = Random.Range(minIntervalTime, maxIntervalTime);
            yield return new WaitForSeconds(time);
            GameObject gObj = Instantiate(prefab);

            FallingObjectInstantiate fallingObject = gObj.GetComponent<FallingObjectInstantiate>();
            //Debug.Log("setting screen shake to " + screenShake);
            fallingObject.screenShake = screenShake;

            instantiatedPrefabs.Add(gObj);
            float height = Random.Range(minHeight, maxHeight);
            gObj.transform.position = playerPosition + new Vector2(0, height);
            Rigidbody2D rb = gObj.GetComponent<Rigidbody2D>();
            float speed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = new Vector2(0, speed);
        }
    }

    private void DestroyInstantiated(Hashtable h)
    {
        playerInCollider = false;
        if (coroutine != null) {
            StopCoroutine(coroutine);   
        }
        foreach (GameObject gObj in instantiatedPrefabs) {
            Destroy(gObj);
        } 
        instantiatedPrefabs = new List<GameObject>();
    }

}
