using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public float fadeTime = 5;

    private bool fading = false;
    private Image image;

    private void Start()
    {
        EventManager.StartListening(Constants.FADE_TO_WHITE, TriggerFade);
        image = GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0f);
    }

    private void TriggerFade(Hashtable h) {
        StartCoroutine(RedrawTexture(5));
    }

    private IEnumerator RedrawTexture(float endTime)
    {
        float counterTime = 0;
        while (counterTime < endTime)
        {
            Color color = new Color(1, 1, 1, counterTime/endTime);
            image.color = color;
            counterTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
