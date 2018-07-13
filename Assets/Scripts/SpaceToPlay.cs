using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpaceToPlay : MonoBehaviour {

	void Update () {
	if (Input.GetKeyUp(KeyCode.Space))
     {
       SceneManager.LoadScene("Main-long", LoadSceneMode.Single);
     }
	}
}
