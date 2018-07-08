using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
