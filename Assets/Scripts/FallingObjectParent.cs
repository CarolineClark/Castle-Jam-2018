using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectParent : MonoBehaviour
{
    public GameObject prefab;

    private static string CHILD_OBJECT_NAME = "SpawnPosition";
    Vector2 spawnPosition;

    void Start()
    {
        spawnPosition = transform.Find(CHILD_OBJECT_NAME).transform.position;
    }

    public void TriggerFall()
    {
        GameObject gObj = Instantiate(prefab);
        gObj.transform.position = spawnPosition;
    }
}
