﻿using System.Collections;
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
        clockHand.transform.rotation = Quaternion.AngleAxis(6*ticks , Vector3.back);
        ticks++;
    }

    public void EndOfCount()
    {
        Debug.Log("End");
    }
}
