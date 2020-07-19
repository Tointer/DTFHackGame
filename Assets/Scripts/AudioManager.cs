using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour, ICount
{
    public AudioSource tick;
    public AudioSource over;
    public AudioSource start;
    public AudioSource succes;

    private static AudioSource soundtrackSource;
    private static float originalSoundtrackVolume;
    
    
    public GameObject soundtrack;
    private float tickFrequency = 0f;
    private IEnumerator fadeIn;
    
    void Start()
    {
        StartCoroutine(CountFrequency());
        var track = FindObjectOfType<Soundtrack>();
        if (track == null)
        {
            var a = Instantiate(soundtrack);
            soundtrackSource = a.GetComponent<AudioSource>();
            originalSoundtrackVolume = soundtrackSource.volume;
        }
        else
        {
            soundtrackSource = track.GetComponent<AudioSource>();
        }
        UnmuteSoundtrack();
    }

    public enum Sounds {GameOver, LevelStart, Succes}

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.GameOver:
                over.Play();
                break;
            case Sounds.LevelStart:
                start.Play();
                break;
            case Sounds.Succes:
                succes.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sound), sound, null);
        }
    }
    
    IEnumerator CountFrequency()
    {
        while (true)
        {
            tickFrequency = Mathf.Clamp(tickFrequency - Time.deltaTime, 0, 1.5f);
            yield return null;
        }
    }
    
    public void Tick()
    {
        tick.pitch = Random.Range(0.4f + tickFrequency, 0.5f + tickFrequency);
        tickFrequency += 0.2f;
        tick.Play();
    }

    public void EndOfCount()
    {
        
    }

    public void MuteSoundtrack()
    {
        if(fadeIn != null)
            StopCoroutine(fadeIn);
        soundtrackSource.volume = 0;
    }

    public void UnmuteSoundtrack()
    {
        if (Math.Abs(soundtrackSource.volume) > 0.02f)
            return;
        fadeIn = FadeInSound(soundtrackSource, 3f, originalSoundtrackVolume);
        StartCoroutine(fadeIn);
    }
    
    IEnumerator FadeInSound(AudioSource fadeAudio, float duration, float targetVolume = 1f)
    {
        if (duration <= 0 || fadeAudio == null) throw new ArgumentException();

        while (fadeAudio.volume <= targetVolume)
        {
            fadeAudio.volume += Time.unscaledDeltaTime/duration;
            yield return null;
        }
    }
}
