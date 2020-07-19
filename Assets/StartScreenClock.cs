using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StartScreenClock : MonoBehaviour
{
    public Transform clockHand;
    private int ticks;
    public static int activeClocks = 0;

    private void Start()
    {
        activeClocks++;
        ticks = Random.Range(0, 57);
        clockHand.transform.rotation = Quaternion.AngleAxis(ticks*6 , Vector3.back);
        StartCoroutine(StartClock());
    }

    IEnumerator StartClock()
    {
        while (true)
        {
            ticks++;
            clockHand.transform.rotation = Quaternion.AngleAxis(6 * ticks, Vector3.back);
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
