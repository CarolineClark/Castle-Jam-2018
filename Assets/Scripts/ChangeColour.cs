using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO - FIX, THIS ISN'T WORKING.
public class ChangeColour : MonoBehaviour
{
    float m_Hue;
    float m_Saturation;
    float m_Value;

    public float timeToComplete;

    private float startSaturation = 1;
    private float endSaturation = 0;
    private float startBrightness = 0.10f;
    private float endBrightness = 1;
    private float timeSoFar = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = Color.HSVToRGB(0.0f, 0, 0);
        spriteRenderer.material.color = color;
        EventManager.StartListening(Constants.STOP_SIGNS_FALLING, LightenBg);
    }

    public void LightenBg(Hashtable h) {
        StartCoroutine(LightenBgCoroutine());
    }

    private IEnumerator LightenBgCoroutine()
    {
        while (timeSoFar < timeToComplete) {
            float transition = timeSoFar / timeToComplete;
            float saturation = Mathf.Lerp(startSaturation, endSaturation, transition);
            float brightness = Mathf.Lerp(startBrightness, endBrightness, transition);
            Color color = Color.HSVToRGB(0.0f, saturation, brightness);
            spriteRenderer.material.color = color;
            timeSoFar += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
