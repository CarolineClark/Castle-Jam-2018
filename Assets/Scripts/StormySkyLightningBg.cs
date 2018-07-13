using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormySkyLightningBg : MonoBehaviour {
    private float minLull = 5f;
    private float maxLull = 1f;
    private float minFlash = 0f;
    private float maxFlash = 0.5f;
    private float minOpacity = 0f;
    private float midOpacity = 0.2f;
    private float maxOpacity = 0.7f;
    private float minFade = 0.05f;
    private float maxFade = 0.15f;
    private GameObject lightningObj;

    private void Start()
    {
        lightningObj = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(LightningFlash());
    }

    private IEnumerator LightningFlash()
    {
        Color tempColor = lightningObj.GetComponent<SpriteRenderer>().material.color;
        tempColor.a = minOpacity;
        lightningObj.GetComponent<SpriteRenderer>().material.color = tempColor;

        while (true)
        {

            yield return new WaitForSeconds(Random.Range(minLull, maxLull));

            for (int x = 0; x < 3; x++)
            {
                float randomOpacity = Random.Range(midOpacity, maxOpacity);
                float randomFade = Random.Range(minFade, maxFade);
                iTween.FadeTo(lightningObj, randomOpacity, randomFade);
                yield return new WaitForSeconds(Random.Range(minFlash, maxFlash));

                randomOpacity = Random.Range(minOpacity, maxOpacity);
                randomFade = Random.Range(minFade, maxFade);
                iTween.FadeTo(lightningObj, minOpacity, randomFade);
                yield return new WaitForSeconds(Random.Range(minFlash, maxFlash));
            }
        }
    }
}
