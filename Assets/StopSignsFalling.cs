﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSignsFalling : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.PLAYER_TAG) {
            Debug.Log("stop all the things");
            CameraController.StopShaking();
            EventManager.TriggerEvent(Constants.STOP_SIGNS_FALLING);
        }
    }
}