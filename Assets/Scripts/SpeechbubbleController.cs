using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechbubbleController : MonoBehaviour {

    private Text speechbubbleText;
    private Image speechbubbleImage;

    private static string SPEECH_BUBBLE_TEXT = "Canvas/SpeechbubbleText"; 
    private static string SPEECH_BUBBLE_IMAGE = "Canvas/SpeechbubbleImage"; 

	void Start () {
        speechbubbleText = transform.Find(SPEECH_BUBBLE_TEXT).GetComponent<Text>();
        speechbubbleImage = transform.Find(SPEECH_BUBBLE_IMAGE).GetComponent<Image>();
	}

    public void ShowWithText(string s) {
        speechbubbleText.text = s;
        SetSpeechbubbleTo(true);
    }

    public void Hide() {
        SetSpeechbubbleTo(false);
    }

    private void SetSpeechbubbleTo(bool shown)
    {
        speechbubbleImage.enabled = shown;
        speechbubbleText.enabled = shown;
    }
}
