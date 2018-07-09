using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectParent : MonoBehaviour
{
    public GameObject prefab;
    public int numberToSpawn = 1;

    private List<GameObject> spawned = new List<GameObject>();

    private static string CHILD_OBJECT_NAME = "SpawnPosition";
    Vector2 spawnPosition;

    void Start()
    {
        spawnPosition = transform.Find(CHILD_OBJECT_NAME).transform.position;
        EventManager.StartListening(Constants.RESTART_GAME, Reset);
    }

    public void TriggerFall()
    {
        if (spawned.Count < numberToSpawn) {
            GameObject gObj = Instantiate(prefab);
            spawned.Add(gObj);
            gObj.transform.position = spawnPosition;    
        }
    }

    private void Reset(Hashtable h) {
        DestroyInstantiated();
        spawned = new List<GameObject>();
    }

    private void DestroyInstantiated()
    {
        foreach(GameObject spawn in spawned) {
            Destroy(spawn);
        }
    }
}
