using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class RocksFallingSceneTest { 
    [SetUp]
    public void Before()
    {
        SceneManager.LoadScene(ConstantsTest.ROCKS_FALLING_SCENE, LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator CheckPlayerFallsThroughCheckpointOnEntry() {
        yield return null;
        GameObject player = Utils.FindPlayerGameObject();
        Assert.AreNotEqual(player, null);
        //CheckpointEvent checkpoint = new CheckpointEvent();
    }
}
