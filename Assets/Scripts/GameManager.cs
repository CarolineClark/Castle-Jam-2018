using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private Vector2 checkpointPosition;
    private PlayerController playerController;

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
        playerController = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<PlayerController>();
        EventManager.StartListening(Constants.PLAYER_DIED_EVENT, StartGame);
    }

    private void CheckpointHit(Hashtable value)
    {
        Debug.Log("Hi the checkpoint was hit.");
        checkpointPosition = CheckpointEvent.ReadCheckpoint(value);
    }

    private void StartGame(Hashtable h)
    {
        playerController.SpawnPlayer(checkpointPosition);
        Debug.Log("game started!");
    }
}
