using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class Utils {
    public static GameObject FindPlayerGameObject()
    {
        return GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
    }

    public static IEnumerator SkipFrames(int number) {
        for (int i = 0; i < number; i++) {
            yield return null;
        }
    }
}
