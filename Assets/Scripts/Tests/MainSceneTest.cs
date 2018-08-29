using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSceneTest {

    [SetUp]
    public void Before() {
        SceneManager.LoadScene(Constants.MAIN_SCENE, LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator IsPlayerPresentInMainScene() {
        yield return null;
        Assert.AreNotEqual(Utils.FindPlayerGameObject(), null);
    }
}
