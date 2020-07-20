using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerClock : MonoBehaviour, ICount
{

    public GameObject clockHand;
    public int debugClockSpeed = 1;
    private int ticks = 0;
    
    
    public void Tick()
    {
        ticks++;
        clockHand.transform.rotation = Quaternion.AngleAxis(6*ticks , Vector3.back);
        if (ticks >= 54) StartCoroutine(Shake());
    }

    public void EndOfCount()
    {
        Debug.Log("End");
    }

    IEnumerator Shake()
    {
        while (true)
        {
            yield return Move(0.05f + (ticks - 54)/150f, new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), 0).normalized);
            
            transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds((60 - ticks)/50f);
        }
    }

    IEnumerator Move(float distance, Vector3 direction)
    {
        while (transform.localPosition.magnitude <= distance)
        {
            transform.Translate(direction*Time.deltaTime);
            yield return null;
        }
    }
}
