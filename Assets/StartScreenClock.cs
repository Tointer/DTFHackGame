using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class StartScreenClock : MonoBehaviour
{
    public Transform clockHand;
    public int ticks;
    public static int activeClocks = 0;
    public AudioSource tickSound;
    public static int playingSound = 0;

    private void Start()
    {
        activeClocks++;
        ticks = Random.Range(0, 57);

        clockHand.transform.rotation = Quaternion.AngleAxis(ticks*6 , Vector3.back);
        StartCoroutine(StartClock());
    }

    IEnumerator StartClock()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            ticks++;
            clockHand.transform.rotation = Quaternion.AngleAxis(6 * ticks, Vector3.back);
            StartCoroutine(PlayWithDelay(tickSound, Random.Range(0f, 0.08f)));
            if (ticks == 60)
            {
                StartCoroutine(FadeOut());
                activeClocks--;
                if(activeClocks <= 0) GameObject.FindObjectOfType<StartScreen>().StartFading();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator PlayWithDelay(AudioSource audio, float delay)
    {
        if (playingSound > 15) yield break;
        
        playingSound++;
        
        yield return new WaitForSeconds(delay);
        audio.pitch = Random.Range(0.6f, 1.2f);
        audio.Play();
        yield return new WaitForSeconds(0.1f);
        
        playingSound--;

    }

    IEnumerator FadeOut()
    {
        var myRend = GetComponent<SpriteRenderer>();
        var handRend = clockHand.GetComponent<SpriteRenderer>();
        var color = myRend.color;

        while (myRend.color.a > 0.1)
        {
            color = myRend.color;
            color.a = Mathf.Clamp01(color.a - Time.deltaTime/2);
            myRend.color = color;
            handRend.color = color;
            yield return null;
        }
    }
}
