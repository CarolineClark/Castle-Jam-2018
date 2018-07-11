using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource footstepSource;
    public AudioSource fxSource;
    public AudioSource startingMusicSource;
    public AudioSource altMusic1Source;
    public AudioSource altMusic2Source;
    public AudioSource altMusic3Source;
    public AudioClip startingMusic;
    public AudioClip altMusic1;
    public AudioClip altMusic2;
    public AudioClip altMusic3;

    private static SoundManager soundManager;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;
    private float musicFullVol = 0.5f;
    private float musicLowestVol = 0f;

    public static SoundManager instance
    {
        get
        {
            if (!soundManager)
            {
                soundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;
                if (!soundManager)
                {
                    Debug.LogError("Need an active SoundManager on a GameObject in your scene");
                }
                else
                {
                    soundManager.Init();
                }
            }
            return soundManager;
        }
    }

    private void Init()
    {
        DontDestroyOnLoad(gameObject);
        InitMusic();
    }

    public void PlayFootstep(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        footstepSource.pitch = randomPitch;
        footstepSource.clip = clips[randomIndex];
        footstepSource.Play();
    }

    public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        fxSource.pitch = randomPitch;
        fxSource.clip = clips[randomIndex];
        fxSource.Play();
    }

    private void InitMusic()
    {
        startingMusicSource.volume = musicFullVol;
        altMusic1Source.volume = musicLowestVol;
        altMusic2Source.volume = musicLowestVol;
        altMusic3Source.volume = musicLowestVol;
        startingMusicSource.clip = startingMusic;
        altMusic1Source.clip = altMusic1;
        altMusic2Source.clip = altMusic2;
        altMusic3Source.clip = altMusic3;
        startingMusicSource.Play();
        altMusic1Source.Play();
        altMusic2Source.Play();
        altMusic3Source.Play();
    }

    public void SetMusic(int musicNumber)
    {
        startingMusicSource.volume = musicLowestVol;
        if (musicNumber == 1)
        {
            altMusic1Source.volume = musicFullVol;
            altMusic2Source.volume = musicLowestVol;
            altMusic3Source.volume = musicLowestVol;
        }
        else if (musicNumber == 2)
        {
            altMusic1Source.volume = musicLowestVol;
            altMusic2Source.volume = musicFullVol;
            altMusic3Source.volume = musicLowestVol;
        }
        else if (musicNumber == 3)
        {
            altMusic1Source.volume = musicLowestVol;
            altMusic2Source.volume = musicLowestVol;
            altMusic3Source.volume = musicFullVol;
        }
    }
}
