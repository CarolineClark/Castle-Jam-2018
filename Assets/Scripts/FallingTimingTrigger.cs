using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTimingTrigger : MonoBehaviour {

    public GameObject prefab;
    public List<float> fallingTimes;

    private Vector2 height = new Vector2(0, 5);
    private bool playerInCollider = false;
    private Vector2 playerPosition;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private void Start()
    {
        EventManager.StartListening(Constants.RESTART_GAME, DestroyInstantiated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) {
            playerInCollider = true;
            StartCoroutine(InstantiateFallingObject());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG)
        {
            playerInCollider = false;
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
            foreach (float time in fallingTimes)
            {
                if (!playerInCollider) {
                    break;
                }
                yield return new WaitForSeconds(time);
                GameObject gObj = Instantiate(prefab);
                instantiatedPrefabs.Add(gObj);
                FallingObjectInstantiate boulder = gObj.GetComponent<FallingObjectInstantiate>();
                boulder.transform.position = playerPosition + height;
            }    
        }
    }

    private void DestroyInstantiated(Hashtable h)
    {
        playerInCollider = false;
        foreach (GameObject gObj in instantiatedPrefabs) {
            Destroy(gObj);
        } 
        instantiatedPrefabs = new List<GameObject>();
    }

}
