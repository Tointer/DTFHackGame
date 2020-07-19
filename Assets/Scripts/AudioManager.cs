using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, ICount
{
    public AudioSource tick;
    public AudioSource timeWarp;

    private float tickFrequency = 0f;
    
    void Start()
    {
        StartCoroutine(CountFrequency());
    }
    
    void Update()
    {
        
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
}
