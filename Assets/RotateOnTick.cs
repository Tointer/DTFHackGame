using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTick : MonoBehaviour, ICount
{
    public Transform rotatingObject;
    public bool counterClockwise;
    
    public void Tick()
    {
        rotatingObject.Rotate(Vector3.back, counterClockwise? -6: 6);
    }

    public void EndOfCount(){
        
    }
}
