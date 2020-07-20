using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEachSecond : MonoBehaviour
{
    public Transform rotatingObject;
    public bool counterClockwise;

    IEnumerator RotateMyObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            rotatingObject.Rotate(Vector3.back, counterClockwise? -6: 6);
        }
    }

    private void Start()
    {
        StartCoroutine(RotateMyObject());
    }
}
