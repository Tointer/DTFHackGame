using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenClock : MonoBehaviour
{
    public Transform clockHand;
    private int ticks;

    private void Start()
    {
        ticks = Random.Range(0, 59);
        clockHand.transform.rotation = Quaternion.AngleAxis(ticks*6 , Vector3.back);
        StartCoroutine(StartClock());
    }

    IEnumerator StartClock()
    {
        while (true)
        {
            ticks++;
            clockHand.transform.rotation = Quaternion.AngleAxis(6 * ticks, Vector3.back);
            yield return new WaitForSeconds(1);
        }
    }
}
