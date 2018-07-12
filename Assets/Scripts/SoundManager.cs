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

    private AudioSource[] musicSources;
    private static SoundManager soundManager;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;
    private float musicFullVol = 0.65f;
    private float musicLowestVol = 0f;
    private float defaultFadeTime = 5f;
    private int currentMusic = 0;
    private int nextMusic = 0;
    private bool fadingMusic = true;

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
        musicSources = new AudioSource[] {
            startingMusicSource,
            altMusic1Source,
            altMusic2Source,
            altMusic3Source
        };
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
        musicSources[0].clip = startingMusic;
        musicSources[1].clip = altMusic1;
        musicSources[2].clip = altMusic2;
        musicSources[3].clip = altMusic3;

        foreach (AudioSource source in musicSources)
        {
            source.volume = musicLowestVol;
            source.Play();
        }

        musicSources[0].volume = musicFullVol;
    }

    public void SetMusic(int music)
    {
        nextMusic = music;
    }

    private void FadeMusicTick()
    {
        if (nextMusic == currentMusic) return;

        if (musicSources[currentMusic].volume > musicLowestVol)
            musicSources[currentMusic].volume -= Time.deltaTime / defaultFadeTime;

        if (musicSources[nextMusic].volume < musicFullVol)
            musicSources[nextMusic].volume += Time.deltaTime / defaultFadeTime;
        else
            currentMusic = nextMusic;
    }
    
    private void FixedUpdate()
    {
        FadeMusicTick();
    }
}
