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
        EventManager.StartListening(Constants.PLAYER_DIED_EVENT, OnPlayerDied);
    }

    private void CheckpointHit(Hashtable value)
    {
        checkpointPosition = CheckpointEvent.ReadCheckpoint(value);
    }

    private void OnPlayerDied(Hashtable h) {
        StartCoroutine(DelayStartGame());
    }
    
    private IEnumerator DelayStartGame() {
        yield return new WaitForSeconds(2.0f);
        StartGame(null);
    }
     
    private void StartGame(Hashtable h)
    {
        playerController.SpawnPlayer(checkpointPosition);
        EventManager.TriggerEvent(Constants.RESTART_GAME);
    }
}
