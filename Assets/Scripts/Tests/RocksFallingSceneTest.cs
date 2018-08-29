using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class RocksFallingSceneTest {

    private bool checkpointHit = false;

    [UnityTest]
    public IEnumerator CheckPlayerExistsInScene() {
        LoadScene();
        IsPlayerPresent();
        yield return Utils.SkipFrames(1);
    }

    [UnityTest]
    public IEnumerator CheckPlayerFallsThroughCheckpointOnEntry()
    {
        LoadScene();
        yield return Utils.SkipFrames(1);
        CheckpointEvent.Listen(CheckpointListener);
        yield return Utils.SkipFrames(10);
        Assert.IsTrue(checkpointHit);
    }

    private void IsPlayerPresent() {
        GameObject player = Utils.FindPlayerGameObject();
        Assert.AreNotEqual(player, null);
    }

    private void LoadScene() {
        SceneManager.LoadScene(Constants.ROCKS_FALLING_SCENE, LoadSceneMode.Single);
    }

    private void CheckpointListener(Hashtable h) {
        checkpointHit = true;
    }
}
