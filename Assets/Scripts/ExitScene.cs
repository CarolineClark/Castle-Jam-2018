using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour {
    public string leavingSceneName;
    public string sceneToGoTo;

	void OnTriggerEnter2D(Collider2D collider) {

        if (SceneManager.GetActiveScene().name == leavingSceneName)
        {
            SceneManager.LoadScene(sceneToGoTo, LoadSceneMode.Single);
        }
	}
}
