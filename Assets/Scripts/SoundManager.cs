using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource footstepSource;
    public AudioSource fxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Awake()
    {
        EnsureSingleton();
        DontDestroyOnLoad(gameObject);
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

    public void PlayFootstep(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        footstepSource.pitch = randomPitch;
        footstepSource.clip = clips[randomIndex];
        footstepSource.Play();
    }
}
