using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // get player, checkpoint, set up events
        CheckpointEvent.Listen(CheckpointHit);
    }

    private void CheckpointHit(Hashtable value)
    {
        Debug.Log("Hi the checkpoint was hit.");
    }

    private void StartGame()
    {
        
    }

    private void Die()
    {
        // show death screen, then restart from checkpoint. (optionally after button)
    }

    private void RestartFromCheckpoint()
    {
        
    }

    void Update () 
    {
        
    }
}
