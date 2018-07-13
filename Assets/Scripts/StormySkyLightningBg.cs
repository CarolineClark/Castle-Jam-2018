using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormySkyLightningBg : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
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
    private GameObject greyBackgroundObj;
    private SpriteRenderer greyBackgroundSR;
    private bool doesLightningFlash = true;
    private Coroutine lightningCoroutine;
    private float timeToFade = 20.0f;
    private float timeCounter;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.StartListening(Constants.STORMY_SKY_TO_GREY, FadeToGrey);
        lightningObj = transform.Find("Lightning").gameObject;
        greyBackgroundObj = transform.Find("GreyBackground").gameObject;
        greyBackgroundSR = greyBackgroundObj.GetComponent<SpriteRenderer>();
        greyBackgroundSR.material.color = new Color(1f, 1f, 1f, 0f);
        lightningCoroutine = StartCoroutine(LightningFlash());
    }

    public void StopLightning() {
        doesLightningFlash = false;
        StopCoroutine(lightningCoroutine);
    }

    private void FadeToGrey(Hashtable h) {
        StopLightning();
        StartCoroutine(FadeToOtherImage());
    }

    private IEnumerator FadeToOtherImage() {
        while(timeCounter < timeToFade) {
            timeCounter += Time.deltaTime + 0.1f;
            float transition = Mathf.Lerp(1, 0, timeCounter / timeToFade);
            spriteRenderer.material.color = new Color(1f,1f,1f, transition);
            greyBackgroundSR.material.color = new Color(1f, 1f, 1f, 1 - transition);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator LightningFlash()
    {
        Color tempColor = lightningObj.GetComponent<SpriteRenderer>().material.color;
        tempColor.a = minOpacity;
        lightningObj.GetComponent<SpriteRenderer>().material.color = tempColor;

        while (doesLightningFlash)
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
