using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class RocksFallingSceneTest {

    private bool checkpointHit = false;
    private InputTest input;

    [SetUp]
    public void Before() {
        input = new InputTest();
    }

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

    [UnityTest]
    public IEnumerator CheckPlayerRespawns()
    {
        LoadScene();
        yield return Utils.SkipFrames(1);
        GameObject player = Utils.FindPlayerGameObject();
        SwitchOutInputMethodOnPlayer();
        yield return Utils.SkipFrames(120);
        Vector2 pos = player.transform.position;
        Vector2 startPos = new Vector3(pos.x, pos.y);
        input.SetLeftPressed(40);
        yield return Utils.SkipFrames(300);
        Vector2 diff = startPos - (Vector2)player.transform.position;
        Assert.LessOrEqual(diff.magnitude, 2.5);
    }

    private void IsPlayerPresent() {
        GameObject player = Utils.FindPlayerGameObject();
        Assert.AreNotEqual(player, null);
    }

    private void SwitchOutInputMethodOnPlayer() {
        GameObject player = Utils.FindPlayerGameObject();
        player.GetComponent<PlayerController>().input.inputClass = input;
    }

    private void LoadScene() {
        SceneManager.LoadScene(Constants.ROCKS_FALLING_SCENE, LoadSceneMode.Single);
    }

    private void CheckpointListener(Hashtable h) {
        checkpointHit = true;
    }
}
