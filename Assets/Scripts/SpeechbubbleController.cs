using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechbubbleController : MonoBehaviour {

    private Text speechbubbleText;
    private Image speechbubbleImage;

    private const float NORMAL_TYPING_SPEED = .03f;
    private const float FAST_TYPING_SPEED = .01f;

    private static string SPEECH_BUBBLE_TEXT = "Canvas/SpeechbubbleText"; 
    private static string SPEECH_BUBBLE_IMAGE = "Canvas/SpeechbubbleImage"; 

	void Start () {
        speechbubbleText = transform.Find(SPEECH_BUBBLE_TEXT).GetComponent<Text>();
        speechbubbleImage = transform.Find(SPEECH_BUBBLE_IMAGE).GetComponent<Image>();
	}

    public void ShowWithText(string textToShow) {
        speechbubbleImage.enabled = true;
        StartCoroutine(AnimateText(textToShow));
    }

    public void Hide() {
        SetSpeechbubbleTo(false);
    }

    private IEnumerator AnimateText(string textToShow)
    {
        speechbubbleText.enabled = true;
        speechbubbleText.text = "";
        for (int i = 0; i < (textToShow.Length + 1); i++)
        {
            speechbubbleText.text = textToShow.Substring(0, i);
            yield return new WaitForSeconds(NORMAL_TYPING_SPEED);
        }
    }

    private void SetSpeechbubbleTo(bool shown)
    {
        speechbubbleImage.enabled = shown;
        speechbubbleText.enabled = shown;
    }
}
