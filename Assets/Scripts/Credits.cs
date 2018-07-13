using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    private void Start() {
        EventManager.StartListening(Constants.END_GAME, Show);
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
    }

    private void Show(Hashtable h)
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = true;
    }
}
