using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class Utils {
    public static GameObject FindPlayerGameObject()
    {
        return GameObject.FindGameObjectWithTag(ConstantsTest.PLAYER_TAG);
    }
}
